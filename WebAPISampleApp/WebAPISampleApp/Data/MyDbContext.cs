using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions option) : base (option) { }

        #region DbSets
        // Represent for Entity in application and create Table in DB
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        #endregion

    }
}
