using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ShopContext shopContext;

        public CategoryRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task<List<Category>> Get()
        {
            List<Category> categories = await shopContext.Categories.ToListAsync();

            return categories;

        }
       

    }
}



