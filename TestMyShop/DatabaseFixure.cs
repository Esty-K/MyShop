using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DatabaseFixure : IDisposable
    {
        public ShopContext Context { get; private set; }
        public DatabaseFixure()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer("Server=srv2\\pupils;Database=214804460_Test;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new ShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureCreated();
            Context.Dispose();
        }
    }
}
