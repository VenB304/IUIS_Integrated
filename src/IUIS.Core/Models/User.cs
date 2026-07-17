using Newtonsoft.Json;

namespace IUIS.Core.Models
{
    /// <summary>
    /// A system login. Stored under the shared "users" node so that every
    /// team's module authenticates against the same account list.
    /// </summary>
    public class User
    {
        /// <summary>Firebase key — held in memory only, never written into the node body.</summary>
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        // TODO (security): passwords are stored in plain text to match the
        // current seed data. Before any real deployment this should hold a
        // salted hash and be verified rather than compared.
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>Determines which modules the dashboard offers. See <c>Roles</c>.</summary>
        [JsonProperty("role")]
        public string Role { get; set; } = string.Empty;

        public override string ToString() => $"{Username} ({Role})";
    }

    /// <summary>
    /// Known role names, centralised so modules compare against a constant
    /// instead of re-typing a magic string.
    /// </summary>
    /// <remarks>
    /// These must match the "role" values actually present in the users node.
    /// The seeded admin account uses "Administrator", not "Admin" — comparing
    /// against the wrong spelling fails silently, which is exactly why the
    /// strings live here rather than being typed out at each call site.
    /// </remarks>
    public static class Roles
    {
        public const string Admin   = "Administrator";
        public const string Staff   = "Staff";
        public const string Faculty = "Faculty";
        public const string Student = "Student";
    }
}
