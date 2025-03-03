using Entity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ShopContext shopContext;

        public UserRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task<User> GetById(int id)
        {
            User user = await shopContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }
        public async Task<User> Post(User user)
        {
            await shopContext.Users.AddAsync(user);
            await shopContext.SaveChangesAsync();
            if(user.UserId!=0)
                return user;
            return null;
        }

        public async Task<User> PostLogin(string email, string password)
        {
            User user = await shopContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }


        public async Task<User> Put(int id, User user)
        {
            user.UserId = id;
            shopContext.Users.Update(user);
            await shopContext.SaveChangesAsync();
            return user;
        }

    }
}



