using IUIS.Core.Modules;
using IUIS.Modules.Team6;

namespace IUIS.Shell
{
    /// <summary>
    /// Declares which modules the dashboard offers.
    ///
    /// This is the single place the integration is wired up. Team 6 is loaded
    /// in-process; the other seven are still standalone executables and get an
    /// <see cref="ExternalProcessModule"/> shim until they convert.
    ///
    /// To promote a team once their module is a class library:
    ///   1. add a ProjectReference in IUIS.Shell.csproj
    ///   2. swap their ExternalTeam(...) line below for `new TeamNModule()`
    /// Nothing else in the shell changes.
    /// </summary>
    public static class ModuleCatalog
    {
        public static ModuleRegistry Build()
        {
            var registry = new ModuleRegistry();

            registry
                .Register(ExternalTeam(1, "Student Management",
                    "Manage student profiles and personal information", "🎓"))
                .Register(ExternalTeam(2, "Teacher Management",
                    "Manage faculty information and subject assignments", "👨‍🏫"))
                .Register(ExternalTeam(3, "Academic Management",
                    "Manage programs, courses, subjects, and curriculum", "📖"))
                .Register(ExternalTeam(4, "Registrar Management",
                    "Manage academic records and student clearances", "🏛️"))
                .Register(ExternalTeam(5, "Library Management",
                    "Manage book inventory and borrowing history", "📚"))

                // Team 6 — the reference in-process module.
                .Register(new Team6Module())

                .Register(ExternalTeam(7, "Medical Services",
                    "Manage medical records and clearances", "⚕️"))
                .Register(ExternalTeam(8, "Enrollment Management",
                    "Manage student enrollment and tuition assessment", "📝"));

            return registry;
        }

        /// <summary>Builds a shim for a team that still ships a standalone .exe.</summary>
        private static ExternalProcessModule ExternalTeam(
            int teamNumber, string displayName, string description, string icon)
        {
            var descriptor = new ModuleDescriptor(
                ModuleId:    $"team{teamNumber}.external",
                Team:        $"Team {teamNumber}",
                DisplayName: displayName,
                Description: description,
                Icon:        icon,
                Order:       teamNumber);

            return new ExternalProcessModule(descriptor, ResolveTeamExe(teamNumber));
        }

        /// <summary>
        /// Finds a sibling team's built executable by walking up from our own
        /// output folder and looking for their project directory.
        ///
        /// Searching beats a fixed "..\..\..\.." chain: the number of levels
        /// depends on build configuration and on whether the team nests their
        /// project inside a solution folder, and a wrong guess fails silently.
        /// </summary>
        private static string ResolveTeamExe(int teamNumber)
        {
            var folder = $"Final_Project_Team_{teamNumber}";
            var exe    = $"{folder}.exe";

            // Both common layouts: repo/<proj>/bin/... and repo/<repo>/<proj>/bin/...
            string[] shapes =
            [
                Path.Combine(folder, "bin", "Debug", "net10.0-windows", exe),
                Path.Combine(folder, folder, "bin", "Debug", "net10.0-windows", exe)
            ];

            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir is not null)
            {
                foreach (var shape in shapes)
                {
                    var candidate = Path.Combine(dir.FullName, shape);
                    if (File.Exists(candidate)) return candidate;
                }

                dir = dir.Parent;
            }

            // Not built yet. Return the most likely path anyway so the module
            // reports a useful "expected it here" message instead of blank.
            return Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "..", shapes[1]);
        }
    }
}
