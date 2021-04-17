using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities;
using WebStoreDomain.Entities.Identity;
using WebStoreDomain.Entities.Orders;

namespace WebStore.DAL.Context
{
    public class WebStoreContext : IdentityDbContext<User, Role, string>
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options) : base(options)
        { 
            
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}

