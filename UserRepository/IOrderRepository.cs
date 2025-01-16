using Entity;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetById(int id);
        Task<Order> Post(Order order);

    }
}