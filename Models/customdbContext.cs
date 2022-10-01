using Microsoft.EntityFrameworkCore;

namespace PM_MS.Models
{
    public class customdbContext : DbContext
    {
        public DbSet<cartlist> getCartListbyUserName { get; set; }

        public DbSet<cartlist> createcartListforUser { get; set; }
        public DbSet<productList> Products { get; set; }

        public static string Connectionstring
        {
            get;
            set;
        }

        public void BuildConnectionstring(string dbstring) { Connectionstring = dbstring; }

        public customdbContext(DbContextOptions<customdbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<productList>(eb =>
            {
                eb.HasKey("productid");
            });
            modelBuilder.Entity<cartlist>(eb =>
            {
                //eb.HasKey("productid");

                eb.HasNoKey();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(Connectionstring))
            {
                optionsBuilder.UseSqlServer(Connectionstring);
            }
        }
    }
}
