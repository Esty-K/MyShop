using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace TestMyShop
{
    public class OrderUnitTest
    {
        [Fact]
        public async Task GetById_ReturnsOrder()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Date = DateTime.Now,
                Sum = 100.0,
                User = new User { UserId = 1, Email = "user@example.com" },
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2 }
                }
            };

            var mockContext = new Mock<ShopContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(new List<Order> { order });

            var orderRepository = new OrderRepository(mockContext.Object);

            // Act
            var result = await orderRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.UserId);
            Assert.Equal(100.0, result.Sum);
            Assert.Equal("user@example.com", result.User.Email);
            Assert.Single(result.OrderItems);
        }

        [Fact]
        public async Task Post_AddsOrder()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Date = DateTime.Now,
                Sum = 100.0,
                User = new User { UserId = 1, Email = "user@example.com" },
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2 }
                }
            };
            var orders = new List<Order>() { order };

            var mockContext = new Mock<ShopContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);
            mockContext.Setup(x => x.Orders.AddAsync(It.IsAny<Order>(), default))
            .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Order>)null);

            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var orderRepository = new OrderRepository(mockContext.Object);

            // Act
            var result = await orderRepository.Post(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.UserId);
            Assert.Equal(100.0, result.Sum);
            Assert.Equal("user@example.com", result.User.Email);
        
        }
    }
}