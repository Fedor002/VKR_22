using Microsoft.AspNetCore.Mvc.Testing;
using VKR_Visik.Classes;
using Xunit;

namespace VKRTestProject
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest1()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
        }
        [Fact]
        public async void TestUsersStatus()
        {
            // Arrange
            var user = _factory.CreateClient();

            // Act https://localhost:44316/Users/UserList
            var respons = await user.GetAsync("/Users/UserList");

            // Assert
            int code = (int) respons.StatusCode;
            Assert.Equal(200, code);
        }
    }
}