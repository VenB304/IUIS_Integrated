using IUIS.Core.Configuration;
using IUIS.Shell.Forms;

namespace IUIS.Shell
{
    internal static class Program
    {
        /// <summary>The main entry point for the Integrated University Information System.</summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Read the config up front. Failing here with a clear message beats
            // failing later inside a Firebase call with an opaque HTTP error.
            try
            {
                _ = FirebaseConfig.Current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}\n\nThe application cannot start without its database configuration.",
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var registry = ModuleCatalog.Build();

            Application.Run(new LoginForm(registry));
        }
    }
}
