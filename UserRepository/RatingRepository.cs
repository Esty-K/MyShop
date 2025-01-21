using Entities;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        ShopContext shopContext;

        public RatingRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }
        public async Task Post(Rating rating)
        {
            await shopContext.Ratings.AddAsync(rating);
            await shopContext.SaveChangesAsync();
            return;
        }
    }
}
