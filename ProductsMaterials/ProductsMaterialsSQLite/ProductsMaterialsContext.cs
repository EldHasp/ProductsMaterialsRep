using ProductsMaterialsSQLite.DB;
using System.Data.Entity;

namespace ProductsMaterialsSQLite
{
    public class ProductsMaterialsContext : DbContext
    {
        public ProductsMaterialsContext() : base("DefaultConnection") { }
        public ProductsMaterialsContext(string connectionName) : base(connectionName) { }


        public DbSet<ProductDB> Products { get; set; }
        public DbSet<MaterialDB> Materials { get; set; }
        public DbSet<MaterialInProductDB> MaterialsInProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDB>().Property(f => f.Timestamp).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }
    }
}
