using Entity;
using System.Runtime.InteropServices;
using System.Text.Json;
using Repositories;
using Zxcvbn;


namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository repository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.repository = categoryRepository;
        }
        public async Task<List<Category>> Get()
        {

            return await repository.Get();
        }
    }
}



