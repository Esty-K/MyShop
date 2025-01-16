using Entity;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> GetById(int id);
        Task<Order> Post(Order order);

    }
}