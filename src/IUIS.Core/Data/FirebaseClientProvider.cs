using Firebase.Database;
using IUIS.Core.Configuration;
using IUIS.Core.Services;

namespace IUIS.Core.Data
{
    /// <summary>
    /// Supplies the one authenticated <see cref="FirebaseClient"/> the whole
    /// application shares.
    ///
    /// The previous design constructed a fresh client inside every repository's
    /// constructor, which meant a new connection per service per module. One
    /// client, created lazily and reused, keeps connection count flat no matter
    /// how many teams plug in.
    /// </summary>
    public static class FirebaseClientProvider
    {
        private static readonly Lock Gate = new();
        private static FirebaseClient? _client;

        /// <summary>The shared client. Created on first use.</summary>
        public static FirebaseClient Client
        {
            get
            {
                if (_client is not null) return _client;

                lock (Gate)
                {
                    return _client ??= new FirebaseClient(
                        FirebaseConfig.Current.DatabaseUrl,
                        new FirebaseOptions
                        {
                            AuthTokenAsyncFactory = () => FirebaseAuthService.Instance.GetTokenAsync()
                        });
                }
            }
        }
    }
}
