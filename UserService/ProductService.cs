using Entity;
using System.Runtime.InteropServices;
using System.Text.Json;
using Repositories;
using Zxcvbn;


namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository repository;

        public ProductService(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public async Task<List<Product>> Get(string? searchName, int? minPrice, int? maxPrice, int?[] categoryIds)
        {

            return await repository.Get(searchName, minPrice, maxPrice, categoryIds);
        }



    }
}



