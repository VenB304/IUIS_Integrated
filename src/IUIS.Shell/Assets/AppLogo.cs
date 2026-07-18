using System.Reflection;

namespace IUIS.Shell.Assets
{
    /// <summary>Loads the official BatStateU logo embedded in this assembly.</summary>
    internal static class AppLogo
    {
        private static Image? _cached;

        public static Image Load()
        {
            if (_cached is not null)
            {
                return _cached;
            }

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("IUIS.Shell.Assets.bsu-logo.png")
                ?? throw new InvalidOperationException("Embedded resource 'IUIS.Shell.Assets.bsu-logo.png' was not found.");

            _cached = Image.FromStream(stream);
            return _cached;
        }
    }
}
