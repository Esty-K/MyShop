using Entity;

namespace DTO
{
    public record OrderDTO(int Id,string UserFirstName, DateTime Date, ICollection<OrderItemDTO> OrderItems);
    public record PostOrderDTO(int UserId, DateTime Date, double? Sum, ICollection<OrderItemDTO> OrderItems);
}