using System.Collections.Generic;
using System.Linq;

namespace VKR_Visik.Classes
{
    public class UserManager
    {
        private readonly List<Users> _users = new();

        public void AddUser(Users user)
        {
            _users.Add(user);
        }

        public Dictionary<string, List<Users>> CategorizeUsersByRole()
        {
            return _users
                .GroupBy(u => u.users_Role ?? "Unknown")
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public List<Users> SearchUsers(string? name = null, string? role = null)
        {
            return _users.Where(u =>
                (string.IsNullOrEmpty(name) || (u.users_FIO?.Contains(name, System.StringComparison.OrdinalIgnoreCase) ?? false)) &&
                (string.IsNullOrEmpty(role) || (u.users_Role?.Equals(role, System.StringComparison.OrdinalIgnoreCase) ?? false))
            ).ToList();
        }
    }
}
