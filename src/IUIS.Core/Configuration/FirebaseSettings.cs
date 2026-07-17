namespace IUIS.Core.Configuration
{
    /// <summary>
    /// Strongly-typed Firebase connection settings, loaded at runtime from
    /// <c>firebase.local.json</c> rather than hard-coded into the assembly.
    ///
    /// Keeping credentials out of source control means the repository can be
    /// public without exposing the database to the world.
    /// </summary>
    public sealed class FirebaseSettings
    {
        /// <summary>Realtime Database URL, including the trailing slash.</summary>
        public string DatabaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Firebase Web API key. Not a secret by design — Google intends this to
        /// ship in client apps — but it lives here so every setting is in one place.
        /// </summary>
        public string WebApiKey { get; set; } = string.Empty;

        /// <summary>Service account e-mail used for Email/Password sign-in.</summary>
        public string ServiceEmail { get; set; } = string.Empty;

        /// <summary>
        /// Service account password. This IS a real secret: it grants write access
        /// to the shared database. Never commit a file containing it.
        /// </summary>
        public string ServicePassword { get; set; } = string.Empty;

        /// <summary>
        /// Throws if any required field is blank, so misconfiguration surfaces at
        /// startup with a clear message instead of as a confusing HTTP 400 later.
        /// </summary>
        public void Validate()
        {
            var missing = new List<string>();

            if (string.IsNullOrWhiteSpace(DatabaseUrl))     missing.Add(nameof(DatabaseUrl));
            if (string.IsNullOrWhiteSpace(WebApiKey))       missing.Add(nameof(WebApiKey));
            if (string.IsNullOrWhiteSpace(ServiceEmail))    missing.Add(nameof(ServiceEmail));
            if (string.IsNullOrWhiteSpace(ServicePassword)) missing.Add(nameof(ServicePassword));

            if (missing.Count > 0)
            {
                throw new InvalidOperationException(
                    $"firebase.local.json is missing required setting(s): {string.Join(", ", missing)}.");
            }
        }
    }
}
