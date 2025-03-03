using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public record UserDTO(int UserId,
                          [EmailAddress(ErrorMessage = "Invalid email address format.")]
                          string Email,

                          [StringLength(20, ErrorMessage = "First name can be between 2 till 20 characters", MinimumLength = 2)]
                          string? FirstName,

                          [StringLength(20, ErrorMessage = "Last name can be between 2 till 20 characters", MinimumLength = 2)]
                          string? LastName);

    public record PostUserDTO([Required(ErrorMessage = "Password is required.")]
                              string Password,

                              [Required(ErrorMessage = "Email is required.")]
                              [EmailAddress(ErrorMessage = "Invalid email address format.")]
                              string Email,

                              [StringLength(20, ErrorMessage = "First name can be between 2 till 20 characters", MinimumLength = 2)]
                              string? FirstName,

                              [StringLength(20, ErrorMessage = "Last name can be between 2 till 20 characters", MinimumLength = 2)]
                              string? LastName);

    }
