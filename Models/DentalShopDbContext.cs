using DentalShop.Areas.Admin.Model;
using DentalShop.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models
{
    public class DentalShopDbContext : IdentityDbContext<AppUser>
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
               "server=localhost;port=3306;user=root;password='';database=dentalshoptwo;",
               new MySqlServerVersion(new Version(8, 0, 11))
           );
        }

  

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> PorductOrders { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductOrdersUserOrders> ProductOrdersUserOrders { get; set; }
    }
}
