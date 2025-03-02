using Entity;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get(string? searchName, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task<double> GetProductPrice(int id);
    }
}