namespace IUIS.Core.Modules
{
    /// <summary>
    /// The metadata the dashboard needs to draw a module's card, without
    /// having to load or launch the module itself.
    /// </summary>
    /// <param name="ModuleId">Stable unique key, e.g. "team6.employee-attendance".</param>
    /// <param name="Team">Owning team, e.g. "Team 6". Shown on the card.</param>
    /// <param name="DisplayName">Card title, e.g. "Employee &amp; Attendance".</param>
    /// <param name="Description">One-line summary shown under the title.</param>
    /// <param name="Icon">A single emoji used as the card glyph.</param>
    /// <param name="Order">Sort position on the dashboard. Lower comes first.</param>
    public sealed record ModuleDescriptor(
        string ModuleId,
        string Team,
        string DisplayName,
        string Description,
        string Icon,
        int    Order);
}
