using System.Text.Json;

namespace IUIS.Core.Configuration
{
    /// <summary>
    /// Locates and loads <see cref="FirebaseSettings"/> from <c>firebase.local.json</c>.
    ///
    /// The file is read once and cached for the lifetime of the process.
    /// It is deliberately NOT compiled into the assembly, so that rotating the
    /// service-account password does not require a rebuild — and so that the
    /// credential never enters git history.
    /// </summary>
    public static class FirebaseConfig
    {
        private const string FileName = "firebase.local.json";

        private static readonly Lock Gate = new();
        private static FirebaseSettings? _cached;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling         = JsonCommentHandling.Skip,
            AllowTrailingCommas         = true
        };

        /// <summary>
        /// The active settings. Loaded on first access; cached thereafter.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The config file could not be found next to the executable.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The file exists but is malformed or incomplete.
        /// </exception>
        public static FirebaseSettings Current
        {
            get
            {
                // Double-checked locking: the fast path avoids taking the lock
                // once the settings are already loaded.
                if (_cached is not null) return _cached;

                lock (Gate)
                {
                    return _cached ??= Load();
                }
            }
        }

        /// <summary>
        /// Overrides the settings in memory. Intended for tests and for tooling
        /// that supplies configuration from somewhere other than the JSON file.
        /// </summary>
        public static void Override(FirebaseSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            settings.Validate();

            lock (Gate)
            {
                _cached = settings;
            }
        }

        private static FirebaseSettings Load()
        {
            var path = Resolve()
                ?? throw new FileNotFoundException(
                    $"Could not find {FileName}.\n\n" +
                    $"Copy 'firebase.example.json' to '{FileName}', fill in the real values, " +
                    "and rebuild. The file is git-ignored on purpose so credentials stay out " +
                    "of the public repository.",
                    FileName);

            FirebaseSettings settings;
            try
            {
                settings = JsonSerializer.Deserialize<FirebaseSettings>(File.ReadAllText(path), JsonOptions)
                    ?? throw new InvalidOperationException($"{path} deserialized to null.");
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    $"{path} is not valid JSON: {ex.Message}", ex);
            }

            settings.Validate();

            // FirebaseClient builds URLs by string concatenation, so a missing
            // trailing slash silently produces malformed request paths.
            if (!settings.DatabaseUrl.EndsWith('/'))
                settings.DatabaseUrl += "/";

            return settings;
        }

        /// <summary>
        /// Looks for the config beside the executable first (the normal case),
        /// then walks up the directory tree — which is what makes <c>dotnet run</c> and
        /// the test runner work without copying the file into every output folder.
        /// The explicit nested check targets the Team 6 submodule folder
        /// (<c>src/Modules/IUIS.Modules.Team6/</c>), which is where the setup
        /// script drops the credential file.
        /// </summary>
        private static string? Resolve()
        {
            var beside = Path.Combine(AppContext.BaseDirectory, FileName);
            if (File.Exists(beside)) return beside;

            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir is not null)
            {
                var candidate = Path.Combine(dir.FullName, FileName);
                if (File.Exists(candidate)) return candidate;

                var nested = Path.Combine(dir.FullName, "src", "Modules", "IUIS.Modules.Team6", FileName);
                if (File.Exists(nested)) return nested;

                dir = dir.Parent;
            }

            return null;
        }
    }
}
