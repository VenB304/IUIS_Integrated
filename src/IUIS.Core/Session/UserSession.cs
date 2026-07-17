using IUIS.Core.Models;

namespace IUIS.Core.Session
{
    /// <summary>
    /// Who is signed in, for the life of the application.
    ///
    /// The shell creates this once at login and hands it to every module it
    /// launches, so no module has to ask for credentials a second time.
    /// </summary>
    public sealed class UserSession
    {
        public UserSession(User user)
        {
            User      = user ?? throw new ArgumentNullException(nameof(user));
            SignedInAt = DateTime.Now;
        }

        /// <summary>The authenticated account.</summary>
        public User User { get; }

        /// <summary>Local time the user signed in.</summary>
        public DateTime SignedInAt { get; }

        public string Username => User.Username;
        public string Role     => User.Role;

        /// <summary>True when the signed-in account holds the given role.</summary>
        public bool IsInRole(string role) =>
            User.Role.Equals(role, StringComparison.OrdinalIgnoreCase);

        /// <summary>True for administrators, who may open every module.</summary>
        public bool IsAdmin => IsInRole(Roles.Admin);
    }
}
