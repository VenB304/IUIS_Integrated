using System.Diagnostics;
using IUIS.Core.Session;

namespace IUIS.Core.Modules
{
    /// <summary>
    /// Adapter for a team's module that is still a standalone .exe.
    ///
    /// This exists so the dashboard is useful before every team has converted to
    /// a class library: their card launches their executable as a separate
    /// process. It is a transitional shim, not the destination — data still
    /// syncs (everyone shares the same Firebase nodes), but the user visibly
    /// leaves the shell, so convert to <see cref="InProcessModule"/> when you can.
    /// </summary>
    public sealed class ExternalProcessModule : IModule
    {
        private readonly string _executablePath;

        /// <param name="descriptor">Card metadata.</param>
        /// <param name="executablePath">
        /// Path to the .exe. Relative paths resolve against the shell's output
        /// folder, which is how sibling team projects are found.
        /// </param>
        public ExternalProcessModule(ModuleDescriptor descriptor, string executablePath)
        {
            Descriptor      = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
            _executablePath = executablePath ?? throw new ArgumentNullException(nameof(executablePath));
        }

        /// <inheritdoc/>
        public ModuleDescriptor Descriptor { get; }

        /// <summary>The resolved absolute path to the executable.</summary>
        public string ResolvedPath =>
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _executablePath));

        /// <summary>False until the owning team has actually built their project.</summary>
        public bool IsAvailable => File.Exists(ResolvedPath);

        /// <inheritdoc/>
        /// <remarks>
        /// A separate process cannot be told who is signed in, so access is not
        /// enforced here — the launched app runs its own login.
        /// </remarks>
        public bool CanAccess(UserSession session) => true;

        /// <inheritdoc/>
        public void Launch(UserSession session, IWin32Window owner)
        {
            if (!IsAvailable)
            {
                throw new ModuleLaunchException(
                    $"The {Descriptor.DisplayName} module has not been built yet.\n\n" +
                    $"Expected to find it at:\n{ResolvedPath}\n\n" +
                    $"Ask {Descriptor.Team} to build their project, or convert their module " +
                    "to a class library so it loads directly into the dashboard.");
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName         = ResolvedPath,
                    UseShellExecute  = true,
                    WorkingDirectory = Path.GetDirectoryName(ResolvedPath)!
                });
            }
            catch (Exception ex)
            {
                throw new ModuleLaunchException(
                    $"Failed to launch the {Descriptor.DisplayName} module: {ex.Message}", ex);
            }
        }
    }
}
