using IUIS.Core.Modules;
using IUIS.Modules.Team6;
using IUIS.Modules.Team8;

namespace IUIS.Shell
{
    /// <summary>
    /// Declares which modules the dashboard offers.
    ///
    /// This is the single place the integration is wired up. Teams 6 and 8 are
    /// loaded in-process; the other six are still standalone executables and get
    /// an <see cref="ExternalProcessModule"/> shim until they convert.
    ///
    /// Teams 4 and 5 use custom exe resolution because their repo folder names
    /// and output exe names differ from the Final_Project_Team_N convention.
    /// Their cards become active automatically once they build their project —
    /// no changes here are required. When they are ready to promote to
    /// in-process:
    ///   1. add a ProjectReference in IUIS.Shell.csproj
    ///   2. swap their ExternalTeamNamed(...) line below for `new TeamNModule()`
    /// Nothing else in the shell changes.
    /// </summary>
    public static class ModuleCatalog
    {
        public static ModuleRegistry Build()
        {
            var registry = new ModuleRegistry();

            registry
                // Team 1 — repo: Team1_StudentManagementModule, project subfolder: Team1_Final_Project
                .Register(ExternalTeamNamed(1, "Student Management",
                    "Manage student profiles and personal information", "🎓",
                    repoFolder:   "Team1_StudentManagementModule",
                    exeName:      "Team1_Final_Project.exe",
                    framework:    "net10.0-windows",
                    nestedFolder: "Team1_Final_Project"))
                // Team 2 — private repo: Team2_Teacher_Management, modernized to net8.0-windows
                .Register(ExternalTeamNamed(2, "Teacher Management",
                    "Manage faculty information and subject assignments", "👨‍🏫",
                    repoFolder:   "Team2_Teacher_Management",
                    exeName:      "Teacher_Management.exe",
                    nestedFolder: "Teacher_Management",
                    framework:    "net8.0-windows"))
                // Team 3 — repo: AcademicManagementSystem, project subfolder: AcademicManagement
                .Register(ExternalTeamNamed(3, "Academic Management",
                    "Manage programs, courses, subjects, and curriculum", "📖",
                    repoFolder:   "AcademicManagementSystem",
                    exeName:      "AcademicManagement.exe",
                    framework:    "net10.0-windows",
                    nestedFolder: "AcademicManagement"))
                // Team 4 — repo: RegistrarManagement, exe: RegistrarManagement.exe
                .Register(ExternalTeamNamed(4, "Registrar Management",
                    "Manage academic records and student clearances", "🏛️",
                    repoFolder: "RegistrarManagement",
                    exeName:    "RegistrarManagement.exe",
                    framework:  "net8.0-windows"))

                // Team 5 — repo: library-management-module, exe: LibraryManagementSystem.exe
                .Register(ExternalTeamNamed(5, "Library Management",
                    "Manage book inventory and borrowing history", "📚",
                    repoFolder: "library-management-module",
                    exeName:    "LibraryManagementSystem.exe",
                    framework:  "net8.0-windows"))

                // Team 6 — the reference in-process module.
                .Register(new Team6Module())

                .Register(ExternalTeam(7, "Medical Services",
                    "Manage medical records and clearances", "⚕️"))

                // Team 8 — converted to an in-process module.
                .Register(new Team8Module());

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
        /// Builds a shim for a team whose repo folder or exe name does not follow
        /// the Final_Project_Team_N convention.
        /// </summary>
        private static ExternalProcessModule ExternalTeamNamed(
            int    teamNumber,
            string displayName,
            string description,
            string icon,
            string repoFolder,
            string exeName,
            string  framework         = "net10.0-windows",
            string? nestedFolder      = null,
            bool    frameworkSubfolder = true)
        {
            var descriptor = new ModuleDescriptor(
                ModuleId:    $"team{teamNumber}.external",
                Team:        $"Team {teamNumber}",
                DisplayName: displayName,
                Description: description,
                Icon:        icon,
                Order:       teamNumber);

            return new ExternalProcessModule(descriptor,
                ResolveNamedExe(repoFolder, exeName, framework, nestedFolder, frameworkSubfolder));
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

        /// <summary>
        /// Finds a team's built executable when their repo folder and exe name
        /// do not follow the Final_Project_Team_N naming convention.
        /// </summary>
        private static string ResolveNamedExe(
            string  repoFolder,
            string  exeName,
            string  framework,
            string? nestedFolder       = null,
            bool    frameworkSubfolder = true)
        {
            // Build the bin path segment.
            // SDK-style projects (.NET 6+):        [nested/]bin/Debug/<tfm>/<exe>
            // Legacy .csproj (.NET Framework):     [nested/]bin/Debug/<exe>  (no TFM folder)
            var binSegment = (nestedFolder, frameworkSubfolder) switch
            {
                (null,   true)  => Path.Combine("bin", "Debug", framework, exeName),
                (null,   false) => Path.Combine("bin", "Debug", exeName),
                ({ } nf, true)  => Path.Combine(nf,   "bin", "Debug", framework, exeName),
                ({ } nf, false) => Path.Combine(nf,   "bin", "Debug", exeName),
            };

            // Three layouts checked in order:
            //   1. Git submodule: src/Modules/<repoFolder>/[nested/]bin/...  (canonical)
            //   2. Sibling clone: <repoFolder>/[nested/]bin/...
            //   3. Nested clone:  <repoFolder>/<repoFolder>/[nested/]bin/...
            string[] shapes =
            [
                Path.Combine("src", "Modules", repoFolder, binSegment),
                Path.Combine("Modules", repoFolder, binSegment),
                Path.Combine(repoFolder, binSegment),
                Path.Combine(repoFolder, repoFolder, binSegment)
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

            // Not built yet — return a descriptive fallback path.
            return Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "..", shapes[0]);
        }
    }
}
