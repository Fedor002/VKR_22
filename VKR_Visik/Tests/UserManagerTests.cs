using NUnit.Framework;
using VKR_Visik.Classes;
using System.Collections.Generic;

namespace VKR_Visik.Tests
{
    [TestFixture]
    public class UserManagerTests
    {
        private UserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new UserManager();
        }

        [Test]
        public void CategorizeUsersByRole_ShouldGroupUsersByRole()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users { users_FIO = "Alice", users_Role = "Admin" },
                new Users { users_FIO = "Bob", users_Role = "User" },
                new Users { users_FIO = "Charlie", users_Role = "Admin" },
                new Users { users_FIO = "David", users_Role = "User" },
                new Users { users_FIO = "Eve", users_Role = null }
            };

            foreach (var user in users)
            {
                _userManager.AddUser(user);
            }

            // Act
            var result = _userManager.CategorizeUsersByRole();

            // Assert
            Assert.That(result["Admin"].Count, Is.EqualTo(2), "There should be 2 Admin users.");
            Assert.That(result["User"].Count, Is.EqualTo(2), "There should be 2 User users.");
            Assert.That(result["Unknown"].Count, Is.EqualTo(1), "There should be 1 user with an unknown role.");
        }

        [Test]
        public void SearchUsers_ShouldFindUsersByNameAndRole()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users { users_FIO = "Alice Johnson", users_Role = "Admin" },
                new Users { users_FIO = "Bob Smith", users_Role = "User" },
                new Users { users_FIO = "Charlie Brown", users_Role = "Admin" }
            };

            foreach (var user in users)
            {
                _userManager.AddUser(user);
            }

            // Act
            var searchByName = _userManager.SearchUsers(name: "Alice");
            var searchByRole = _userManager.SearchUsers(role: "Admin");

            // Assert
            Assert.That(searchByName.Count, Is.EqualTo(1), "Search by name should return 1 result.");
            Assert.That(searchByName[0].users_FIO, Is.EqualTo("Alice Johnson"), "Search result should match the name.");

            Assert.That(searchByRole.Count, Is.EqualTo(2), "Search by role should return 2 results.");
            Assert.That(searchByRole.Exists(u => u.users_FIO == "Alice Johnson"), "Alice should be in the search results for Admin.");
            Assert.That(searchByRole.Exists(u => u.users_FIO == "Charlie Brown"), "Charlie should be in the search results for Admin.");
        }
    }
}
