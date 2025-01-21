using Entities;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task Post(Rating rating);
    }
}