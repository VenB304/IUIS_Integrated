using IUIS.Core.Modules;
using IUIS.Core.Session;
using IUIS.Modules.Team6.Forms;

namespace IUIS.Modules.Team6
{
    /// <summary>
    /// Team 6's entry point into the shell: Employee &amp; Attendance Management.
    ///
    /// This is the reference implementation the other seven teams should copy.
    /// The whole contract is a descriptor plus one factory method — the shell
    /// handles windowing, errors, and the dashboard card.
    /// </summary>
    public sealed class Team6Module : InProcessModule
    {
        /// <inheritdoc/>
        public override ModuleDescriptor Descriptor { get; } = new(
            ModuleId:    "team6.employee-attendance",
            Team:        "Team 6",
            DisplayName: "Employee & Attendance",
            Description: "Manage non-teaching employee records, departments, and attendance",
            Icon:        "👥",
            Order:       6);

        /// <inheritdoc/>
        protected override Form CreateMainForm(UserSession session) =>
            new EmployeeAttendanceForm(session);
    }
}
