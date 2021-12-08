using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL
{
    public class AuctionDbContext:DbContext
    {
        public DbSet<Login> Logins { get; set; }    
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<ShopptingCart> ShoppingCarts { get; set;
        }
        public AuctionDbContext() : base("AuctionDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
                


            

        }
    }
}
