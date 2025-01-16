using Entity;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> Get(string? searchName, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}