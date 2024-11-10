using MyShop;
using System.Runtime.InteropServices;
using System.Text.Json;
using Repositories;
using Zxcvbn;


namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User Post(User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score < 3)
                return null;
            return repository.Post(user);
        }

        public User PostLogin(string email, string password)
        {
            return repository.PostLogin(email, password);
        }


        public User Put(int id, User userToUpdate)
        {
            return repository.Put(id, userToUpdate);
        }

    }
}



