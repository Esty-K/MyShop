using Entity;

namespace Services
{
    public interface IUserService
    {
        void Delete(int id);
        Task<User> Post(User user);
        Task<User> PostLogin(string email, string password);
        int PostPassword(string password);
        Task<User> Put(int id, User userToUpdate);
    }
}