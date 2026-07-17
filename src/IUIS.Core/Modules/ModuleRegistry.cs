using IUIS.Core.Session;

namespace IUIS.Core.Modules
{
    /// <summary>
    /// The ordered collection of modules the dashboard renders.
    ///
    /// The shell populates this at startup. Keeping registration in one place
    /// means adding a ninth module — or promoting a team from .exe to in-process —
    /// is a one-line change that touches no form code.
    /// </summary>
    public sealed class ModuleRegistry
    {
        private readonly List<IModule> _modules = [];

        /// <summary>Every registered module, in dashboard display order.</summary>
        public IReadOnlyList<IModule> Modules =>
            _modules.OrderBy(m => m.Descriptor.Order).ToList();

        /// <summary>Adds a module. Later registrations replace an existing same-ID module.</summary>
        public ModuleRegistry Register(IModule module)
        {
            ArgumentNullException.ThrowIfNull(module);

            // Replacing rather than throwing lets a team's real module override a
            // placeholder registration without the shell caring about ordering.
            _modules.RemoveAll(m => m.Descriptor.ModuleId == module.Descriptor.ModuleId);
            _modules.Add(module);

            return this;
        }

        /// <summary>The modules this user is allowed to see.</summary>
        public IReadOnlyList<IModule> GetAccessible(UserSession session)
        {
            ArgumentNullException.ThrowIfNull(session);
            return Modules.Where(m => m.CanAccess(session)).ToList();
        }

        /// <summary>Finds a module by its stable ID, or null.</summary>
        public IModule? Find(string moduleId) =>
            _modules.FirstOrDefault(m => m.Descriptor.ModuleId == moduleId);
    }
}
