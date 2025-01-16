using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public partial class User
{
    public int UserId { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    [StringLength(20, ErrorMessage = "lastName can be between 2 till 20", MinimumLength = 2)]
    public string? FirstName { get; set; }
    [StringLength(20, ErrorMessage = "lastName can be between 2 till 20", MinimumLength = 2)]
    public string? LastName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
