using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ShopContext shopContext;

        public OrderRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }


        public async Task<Order> GetById(int id)
        {
            Order order = await shopContext.Orders.Include(o=>o.User).Include(o=>o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }
        public async Task<Order> Post(Order order)
        {
            await shopContext.Orders.AddAsync(order);
            await shopContext.SaveChangesAsync();
            order = await shopContext.Orders.Include(o =>o.User).FirstOrDefaultAsync(o => o.Id == order.Id);

            if(order.Id!=0)
                 return order;
            return null;
        }

     
    }
}
