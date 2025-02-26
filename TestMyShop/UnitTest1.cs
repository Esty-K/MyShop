using System.Reflection.Metadata;
using Entity;
using Repositories;
using Moq;
using Moq.EntityFrameworkCore;
namespace TestMyShop
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUser_ValidCredentials_RerurnsUser()
        {
            var user = new User { Email = "string@vv", Password = "Effwhqdsfes34535bgbg" };
            var mockContext = new Mock<ShopContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.PostLogin(user.Email, user.Password);

            Assert.Equal(user, result);
        }
    }
}