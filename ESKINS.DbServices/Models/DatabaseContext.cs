using ESKINS.DbServices.Models.CMS;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.DbServices.Models
{
    public class DatabaseContext : DbContext
    {
        #region Constructor

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        #endregion

        #region DbSets

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }
        public virtual DbSet<Exteriors> Exteriors { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<ItemCollections> ItemCollections { get; set; }
        public virtual DbSet<ItemLocations> ItemLocations { get; set; }
        public virtual DbSet<ItemLogs> ItemLogs { get; set; }
        public virtual DbSet<ItemPriceHistories> ItemPriceHistories { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<Phases> Phases { get; set; }
        public virtual DbSet<Qualities> Qualities { get; set; }
        public virtual DbSet<Sellers> Sellers { get; set; }
        public virtual DbSet<SoldItems> SoldItems { get; set; }
        public virtual DbSet<Targets> Targets { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<BuyCart> BuyCarts { get; set; }
        public virtual DbSet<SaleCart> SaleCart { get; set; }

        #endregion
    }
}
