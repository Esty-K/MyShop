using Entity;

namespace Repositories
{
    public interface IUserRepository
    {
        void Delete(int id);
        Task<User> Post(User user);
        Task<User> PostLogin(string email, string password);
        Task<User> Put(int id, User userToUpdate);
    }
}