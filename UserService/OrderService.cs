using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository repository;
        IProductRepository productRepository;

        public OrderService(IOrderRepository repository, IProductRepository productRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
        }


        public async Task<Order> GetById(int id)
        {
    
            return await repository.GetById(id);
        }

        public async Task<Order> Post(Order order)
        {

            if (await CheckSum((order.OrderItems).ToList()) != order.Sum)
                return null;
            return await repository.Post(order);
        }
        private async Task<double> CheckSum(List<OrderItem> orderItems)
        {
            double sum = 0;
            foreach (var item in orderItems)
            {
                sum += await productRepository.GetProductPrice(item.ProductId) * item.Quantity;
            }
            return sum;
        }
    }
}
