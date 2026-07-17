using IUIS.Core.Session;

namespace IUIS.Core.Modules
{
    /// <summary>
    /// The contract every team's module implements so the shell can present and
    /// launch it without knowing anything about how it was built.
    ///
    /// Two implementations ship in Core:
    /// <list type="bullet">
    ///   <item><see cref="InProcessModule"/> — the module is a class library
    ///   referenced by the shell and opens as a form inside the running app.
    ///   This is the target state for all eight teams.</item>
    ///   <item><see cref="ExternalProcessModule"/> — the module is still a
    ///   standalone .exe and gets launched as a separate process. A stop-gap so
    ///   the dashboard works before a team has converted.</item>
    /// </list>
    ///
    /// The dashboard only ever sees <c>IModule</c>, so a team can migrate from
    /// one to the other and no shell code changes.
    /// </summary>
    public interface IModule
    {
        /// <summary>Metadata for the dashboard card.</summary>
        ModuleDescriptor Descriptor { get; }

        /// <summary>
        /// False when the module cannot currently be opened — typically an
        /// external .exe that has not been built yet. The dashboard greys the
        /// card out rather than letting the click fail.
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// True when the signed-in user is permitted to open this module.
        /// </summary>
        bool CanAccess(UserSession session);

        /// <summary>
        /// Opens the module.
        /// </summary>
        /// <param name="session">The signed-in user, passed through so the module need not re-authenticate.</param>
        /// <param name="owner">The dashboard, used as the parent window.</param>
        /// <exception cref="ModuleLaunchException">The module could not be opened.</exception>
        void Launch(UserSession session, IWin32Window owner);
    }

    /// <summary>
    /// Thrown when a module fails to open. The shell catches this and shows the
    /// message to the user, so implementations should keep it human-readable.
    /// </summary>
    public sealed class ModuleLaunchException : Exception
    {
        public ModuleLaunchException(string message) : base(message) { }
        public ModuleLaunchException(string message, Exception inner) : base(message, inner) { }
    }
}
