using MyShop;

namespace Services
{
    public interface IUserService
    {
        User Post(User user);
        int PostPassword(string password);
        User PostLogin(string email, string password);
        User Put(int id, User userToUpdate);
        
    }
}