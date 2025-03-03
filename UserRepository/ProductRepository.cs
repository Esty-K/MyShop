﻿using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Repositories
{
    public class ProductRepository :IProductRepository
    {
        ShopContext shopContext;

        public ProductRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task<List<Product>> Get(string? searchName, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = shopContext.Products.Where(product =>
                ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((searchName == null) ? (true) : (product.Name.Contains(searchName)))
                && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
               .OrderBy(product => product.Price).Include(p => p.Category);
        
             List <Product> products = await query.ToListAsync();
            return products;

        }
        public async Task<double> GetProductPrice(int id)
        {
            Product product = await shopContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product.Price;

        }
    }
}



