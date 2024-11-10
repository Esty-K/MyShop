using MyShop;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        string filePath = "M:\\webApi\\MyShop\\MyShop\\users.txt";

        public IEnumerable<string> Get()
        {
            return new string[] { "Shbat", "Shalom" };
        }


        public string GetById(int id)
        {
            return "value";
        }


        public User Post(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
            user.UserId = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
            return user;
        }


        public User PostLogin(string email, string password)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.Email == email && user.Password == password)
                        return user;
                }
            }
            return null;

        }

        public User Put(int id, User userToUpdate)
        {
            userToUpdate.UserId = id;
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.UserId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(filePath, text);
                return userToUpdate;
            }
            return null;

        }


        public void Delete(int id)
        {
        }
    }
}



