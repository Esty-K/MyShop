using Entity;
using System.Runtime.InteropServices;
using System.Text.Json;
using Repositories;
using Zxcvbn;


namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository repository;

        public UserService(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }

        public async Task<User> GetById(int id)
        {
            return await repository.GetById(id);
        }
        public async Task<User> Post(User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);

            if (result.Score < 3)
            {
                return null;
            }
            return await repository.Post(user);
        }


        public async Task<User> PostLogin(string email, string password)
        {
            return await repository.PostLogin(email, password);
        }


        public int PostPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);

            return result.Score;
        }
        public async Task<User> Put(int id, User userToUpdate)
        {
            var result = Zxcvbn.Core.EvaluatePassword(userToUpdate.Password);

            if (result.Score < 3)
            {
                return null;
            }
            return await repository.Put(id, userToUpdate);
        }



    }
}



