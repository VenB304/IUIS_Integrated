using System;
using System.IO;
using System.Text.Json;

namespace IUIS.Modules.Team8.Services
{
    public static class AppSettings
    {
        private static string _dbMode = "Json";
        private static string _firebaseUrl = "https://your-project-id-default-rtdb.firebaseio.com/";
        private static string _firebaseSecret = "";

        public static string DbMode => _dbMode;
        public static string FirebaseUrl => _firebaseUrl;
        public static string FirebaseSecret => _firebaseSecret;

        static AppSettings()
        {
            LoadSettings();
        }

        public static void LoadSettings()
        {
            try
            {
                string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

                if (File.Exists(settingsPath))
                {
                    string json = File.ReadAllText(settingsPath);
                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;
                        if (root.TryGetProperty("DbMode", out JsonElement modeElement))
                        {
                            _dbMode = modeElement.GetString() ?? "Json";
                        }
                        if (root.TryGetProperty("FirebaseUrl", out JsonElement urlElement))
                        {
                            _firebaseUrl = urlElement.GetString() ?? "";
                            if (!string.IsNullOrEmpty(_firebaseUrl) && !_firebaseUrl.EndsWith("/"))
                            {
                                _firebaseUrl += "/";
                            }
                        }
                        if (root.TryGetProperty("FirebaseSecret", out JsonElement secretElement))
                        {
                            _firebaseSecret = secretElement.GetString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load appsettings.json: {ex.Message}");
            }
        }
    }
}
