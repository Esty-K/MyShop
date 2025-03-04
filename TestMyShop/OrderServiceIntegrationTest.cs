using Entity;
using Repositories;
using Services;
using Tests;

namespace TestMyShop
{
    public class OrderServiceIntegrationTest : IClassFixture<DatabaseFixure>
    {
        private readonly ShopContext _context;
        private readonly OrderService _service;
        public OrderServiceIntegrationTest(DatabaseFixure fixture)
        {
            _context = fixture.Context;
            _service = new OrderService(new OrderRepository(_context), new ProductRepository(_context));
        }
        [Fact]
        public async Task Post_ShouldSaveOrder_WithCorrectTotalAmount()
        {
            // Arrange: Create a category for products (because of Foreign Key)
            var category = new Category { Name = "Electronics" };                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Create products with valid category
            var product1 = new Product { Name = "Laptop", Price = 1000, Image = "laptop.jpg", Category = category };
            var product2 = new Product { Name = "Phone", Price = 500, Image = "phone.jpg", Category = category };
            var product3 = new Product { Name = "Tablet", Price = 300, Image = "tablet.jpg", Category = category };

            _context.Products.AddRange(product1, product2, product3);
            await _context.SaveChangesAsync();

            // Create user (because UserId is required in order)
            var user = new User { Email = "test@example.com", Password = "geyt6gG", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Create order with valid products and UserId
            var order = new Order
            {
                Date = DateTime.Now,
                Sum = 2300,  // This should be calculated by checkSum
                UserId = user.UserId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = product1.Id, Quantity = 1 },
                    new OrderItem { ProductId = product2.Id, Quantity = 2 },
                    new OrderItem { ProductId = product3.Id, Quantity = 1 }
                }
            };

            // Act: Send the order to the function being tested
            var savedOrder = await _service.Post(order);

            // Assert: Check data validity
            Assert.NotNull(savedOrder);
            Assert.Equal(2300, savedOrder.Sum); // 1000 + (500*2) + 300 = 1800
            Assert.Equal(3, savedOrder.OrderItems.Count); // 3 products in order
            Assert.Equal(user.UserId, savedOrder.UserId); // Check that user is associated with order
        }

    }
}
