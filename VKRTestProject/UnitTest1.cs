using Microsoft.AspNetCore.Mvc.Testing;
using VKR_Visik.Classes;
using System.Net.Http;
using Xunit;

namespace VKRTestProject
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;

        public UnitTest1()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = _factory.CreateClient(); 
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

        [Theory]
        [InlineData("CBT")]
        [InlineData("ER")]
        public async Task TestForSections(string title)
        {
            //Arrange

            //Act
            var response = await httpClient.GetAsync("./");
            var pageContent = await response.Content.ReadAsStringAsync();
            string contentString = pageContent.ToString();
            //Assert
            Assert.Contains(title, contentString);

        }

    }
}