using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PointOfSaleApp.Entities;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}


    public ApplicationDbContext(){}

    public DbSet<Item> Items {get; set;}
    public DbSet<Receipt> Receipts {get; set;}
    public DbSet<Purchase> Purchases {get; set;}


    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //    modelBuilder.Entity<Item>().HasMany(i => i.Purchases).WithMany(p => p.Items);
    //    modelBuilder.Entity<Receipt>().HasMany(r => r.Purchases).WithMany(p => p.Receipts);
    }


    
}