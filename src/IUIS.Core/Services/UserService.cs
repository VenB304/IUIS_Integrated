using IUIS.Core.Data;
using IUIS.Core.Models;

namespace IUIS.Core.Services
{
    /// <summary>
    /// CRUD plus login validation for the shared "users" node.
    /// This authenticates the person signing in at the login screen.
    /// </summary>
    public class UserService : FirebaseRepository<User>
    {
        protected override string Node     => "users";
        protected override string IdPrefix => "USR";

        protected override string GetId(User entity)          => entity.UserId;
        protected override void   SetId(User entity, string id) => entity.UserId = id;

        /// <summary>
        /// Returns the matching user, or null when the credentials are wrong.
        /// Username is compared case-insensitively; the password is not.
        /// </summary>
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            var users = await GetAllAsync();

            return users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}
