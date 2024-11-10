using MyShop;

namespace Repositories
{
    public interface IUserRepository
    {
        void Delete(int id);
        IEnumerable<string> Get();
        string GetById(int id);
        User Post(User user);
        User PostLogin(string email, string password);
        User Put(int id, User userToUpdate);
    }
}