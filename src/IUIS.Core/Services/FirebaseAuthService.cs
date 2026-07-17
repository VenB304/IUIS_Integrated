using System.Net.Http;
using System.Text;
using IUIS.Core.Configuration;
using Newtonsoft.Json;

namespace IUIS.Core.Services
{
    /// <summary>
    /// Handles Firebase Email/Password authentication via the Firebase Auth REST API.
    /// Signs in with the team service account and refreshes the ID token before it
    /// expires (tokens last one hour; this refreshes five minutes early).
    ///
    /// This authenticates the *application* to the database. It is separate from
    /// <see cref="UserService"/>, which authenticates the *person* at the login screen.
    ///
    /// Usage: <c>await FirebaseAuthService.Instance.GetTokenAsync()</c>, passed as the
    /// AuthTokenAsyncFactory in FirebaseOptions.
    /// </summary>
    public sealed class FirebaseAuthService
    {
        // ── Singleton ─────────────────────────────────────────────────────
        public static readonly FirebaseAuthService Instance = new();
        private FirebaseAuthService() { }

        // ── HTTP client (shared, thread-safe) ─────────────────────────────
        private static readonly HttpClient Http = new();

        // ── Endpoints ─────────────────────────────────────────────────────
        private const string SignInUrl  = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={0}";
        private const string RefreshUrl = "https://securetoken.googleapis.com/v1/token?key={0}";

        // ── Cached token state ────────────────────────────────────────────
        private string?  _idToken;
        private string?  _refreshToken;
        private DateTime _tokenExpiry = DateTime.MinValue;

        // Refresh this far ahead of real expiry, so a request never races the clock.
        private static readonly TimeSpan RefreshBuffer = TimeSpan.FromMinutes(5);

        // Serialises concurrent callers: without this, several modules loading at
        // once would each kick off their own sign-in for the same token.
        private static readonly SemaphoreSlim SignInGate = new(1, 1);

        /// <summary>
        /// Returns a valid Firebase ID token, signing in or refreshing as needed.
        /// </summary>
        public async Task<string> GetTokenAsync()
        {
            if (IsTokenUsable) return _idToken!;

            await SignInGate.WaitAsync();
            try
            {
                // Another caller may have refreshed while we waited on the gate.
                if (IsTokenUsable) return _idToken!;

                if (_refreshToken is not null)
                {
                    try
                    {
                        await RefreshTokenAsync();
                        return _idToken!;
                    }
                    catch
                    {
                        // Refresh token rejected (revoked or expired) — discard the
                        // cached state and fall through to a full sign-in.
                        _refreshToken = null;
                        _idToken      = null;
                    }
                }

                await SignInAsync();
                return _idToken!;
            }
            finally
            {
                SignInGate.Release();
            }
        }

        private bool IsTokenUsable =>
            _idToken is not null && DateTime.UtcNow < _tokenExpiry - RefreshBuffer;

        // ── Private helpers ───────────────────────────────────────────────

        private async Task SignInAsync()
        {
            var settings = FirebaseConfig.Current;

            var url  = string.Format(SignInUrl, settings.WebApiKey);
            var body = Serialize(new
            {
                email             = settings.ServiceEmail,
                password          = settings.ServicePassword,
                returnSecureToken = true
            });

            var response = await Http.PostAsync(url, body);
            await EnsureSuccessAsync(response, "Email/Password sign-in");

            var raw    = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponse>(raw)
                         ?? throw new InvalidOperationException("Empty sign-in response.");

            ApplyToken(result.IdToken, result.RefreshToken, result.ExpiresIn);
        }

        private async Task RefreshTokenAsync()
        {
            var url  = string.Format(RefreshUrl, FirebaseConfig.Current.WebApiKey);
            var body = Serialize(new { grant_type = "refresh_token", refresh_token = _refreshToken });

            var response = await Http.PostAsync(url, body);
            await EnsureSuccessAsync(response, "Token refresh");

            var raw    = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RefreshResponse>(raw)
                         ?? throw new InvalidOperationException("Empty refresh response.");

            ApplyToken(result.IdToken, result.RefreshToken, result.ExpiresIn);
        }

        private void ApplyToken(string idToken, string refreshToken, string expiresIn)
        {
            _idToken      = idToken;
            _refreshToken = refreshToken;
            _tokenExpiry  = DateTime.UtcNow.AddSeconds(
                int.TryParse(expiresIn, out var secs) ? secs : 3600);
        }

        private static StringContent Serialize(object obj) =>
            new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        private static async Task EnsureSuccessAsync(HttpResponseMessage response, string context)
        {
            if (response.IsSuccessStatusCode) return;

            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Firebase {context} failed ({(int)response.StatusCode}): {body}");
        }

        // ── Private DTOs (Firebase Auth REST API response shapes) ─────────

        private sealed class SignInResponse
        {
            [JsonProperty("idToken")]
            public string IdToken { get; set; } = string.Empty;

            [JsonProperty("refreshToken")]
            public string RefreshToken { get; set; } = string.Empty;

            /// <summary>Seconds until expiry, returned as a string by Firebase.</summary>
            [JsonProperty("expiresIn")]
            public string ExpiresIn { get; set; } = "3600";
        }

        private sealed class RefreshResponse
        {
            // The refresh endpoint answers in snake_case, unlike sign-in.
            [JsonProperty("id_token")]
            public string IdToken { get; set; } = string.Empty;

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; } = string.Empty;

            [JsonProperty("expires_in")]
            public string ExpiresIn { get; set; } = "3600";
        }
    }
}
