using System.Data.Entity;

namespace ProductsMaterialsSQLite
{
    public class ProductsMaterialsContext : DbContext
    {
        public ProductsMaterialsContext() : base("DefaultConnection") { }
        public ProductsMaterialsContext(string connectionName) : base(connectionName) { }


        public DbSet<ProductDB> Products { get; set; }
        public DbSet<MaterialDB> Materials { get; set; }
        public DbSet<MaterialsInProductsDB> MaterialsInProducts { get; set; }
    }
}
