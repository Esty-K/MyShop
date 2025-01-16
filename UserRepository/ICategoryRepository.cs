using Entity;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> Get();

    }
}