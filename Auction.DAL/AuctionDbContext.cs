using Auction.DAL.Models;
using System.Data.Entity;

namespace Auction.DAL
{
    public class AuctionDbContext:DbContext
    {
     

     
        public DbSet<Stake> Stakes { get; set; }
        public DbSet<Picture> Pictures { get; set; }    
        public DbSet<Login> Logins { get; set; }    
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<ShopptingCart> ShoppingCarts { get; set;
        }
        public AuctionDbContext() : base("AuctionDb")
        { 
         
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }



    }
}
