using IUIS.Core.Configuration;
using IUIS.Modules.Team6.Forms;

namespace IUIS.Modules.Team6
{
    /// <summary>
    /// Standalone entry point for the Employee &amp; Attendance module.
    ///
    /// Running this project directly (F5) opens the module on its own, with no
    /// login and no dashboard — the quick path when working on these forms.
    ///
    /// When the module is launched from the shell instead, this Main is never
    /// called: <see cref="Team6Module"/> constructs the form directly and hands
    /// it the signed-in session.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Same up-front config check the shell does: fail with a readable
            // message rather than an opaque HTTP error on the first query.
            try
            {
                _ = FirebaseConfig.Current;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}\n\nThe module cannot start without its database configuration.",
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // No session: standalone mode skips the login gate, which belongs to
            // the shell. The form marks itself accordingly.
            Application.Run(new EmployeeAttendanceForm());
        }
    }
}
