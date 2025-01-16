namespace DTO
{
    public record UserDTO(int UserId,string Email, string? FirstName, string? LastName);
    public record PostUserDTO(string Password, string Email, string? FirstName, string? LastName);
}