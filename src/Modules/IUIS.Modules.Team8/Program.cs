using IUIS.Modules.Team8.Forms;

namespace IUIS.Modules.Team8
{
    /// <summary>
    /// Standalone entry point for the Enrollment Management module.
    ///
    /// Running this project directly (F5) opens the module on its own, with no
    /// login and no dashboard — the quick path when working on these forms.
    ///
    /// When the module is launched from the shell instead, this Main is never
    /// called: <see cref="Team8Module"/> constructs the form directly and hands
    /// it the signed-in session.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Homepage());
        }
    }
}
