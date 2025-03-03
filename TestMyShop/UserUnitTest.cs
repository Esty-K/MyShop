using System.Reflection.Metadata;
using Entity;
using Repositories;
using Moq;
using Moq.EntityFrameworkCore;
namespace TestMyShop
{
    public class UserUnitTest
    {
        [Fact]
        public async Task GetUser_ValidCredentials_RerurnsUser()
        {
            // Arrange
            var user = new User { Email = "user@example.com", Password = "Effwhqdsfes34535bgbg" };
            var mockContext = new Mock<ShopContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.PostLogin(user.Email, user.Password);

            // Assert
            Assert.Equal(user, result);
        }
    }
}