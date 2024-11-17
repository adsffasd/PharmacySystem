using Microsoft.EntityFrameworkCore;
using PharmacySystem.Models;

namespace PharmacySystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImpRepo> ImpRepos { get; set; }
        public DbSet<ImpRepoDetail> impRepoDetails { get; set; }
        public DbSet<ExpRepo> ExpRepos { get; set; }

    }
}
