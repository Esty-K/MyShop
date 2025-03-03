using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace TestMyShop
{
    public class CategoryUnitTest
    {
        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" }
            };

            var mockContext = new Mock<ShopContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.Get();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Category1", result[0].Name);
            Assert.Equal("Category2", result[1].Name);
        }
    }
}