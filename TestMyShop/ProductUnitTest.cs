using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace TestMyShop
{
    public class ProductUnitTest
    {
        [Fact]
        public async Task Get_ReturnsFilteredProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 100, CategoryId = 1 },
                new Product { Id = 2, Name = "Product2", Price = 200, CategoryId = 2 },
                new Product { Id = 3, Name = "Product3", Price = 300, CategoryId = 1 }
            };

            var mockContext = new Mock<ShopContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productRepository = new ProductRepository(mockContext.Object);

            // Act
            var result = await productRepository.Get("Product", 100, 300, new int?[] { 1 });

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Id == 1);
            Assert.Contains(result, p => p.Id == 3);
        }
    }
}