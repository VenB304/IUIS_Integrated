using IUIS.Core.Modules;
using IUIS.Core.Session;
using IUIS.Modules.Team8.Forms;

namespace IUIS.Modules.Team8
{
    /// <summary>
    /// Team 8's entry point into the shell: Enrollment Management.
    /// </summary>
    public sealed class Team8Module : InProcessModule
    {
        /// <inheritdoc/>
        public override ModuleDescriptor Descriptor { get; } = new(
            ModuleId:    "team8.enrollment",
            Team:        "Team 8",
            DisplayName: "Enrollment Management",
            Description: "Manage student enrollment and tuition assessment",
            Icon:        "📝",
            Order:       8);

        /// <inheritdoc/>
        protected override Form CreateMainForm(UserSession session) =>
            new Homepage(session);
    }
}
