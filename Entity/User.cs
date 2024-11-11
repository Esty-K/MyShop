using System.ComponentModel.DataAnnotations;

namespace MyShop
{
    public class User
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(20, ErrorMessage = "name can be between 2 till 20", MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "lastName can be between 2 till 20", MinimumLength = 2)]
        public string LastName { get; set; }
        public int UserId { get; set; } = 0;
    }
}
