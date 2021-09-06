using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AddressAPI.Models
{
    /// <summary>
    /// Represents the database connection to the Sqlite.
    /// </summary>
    public class AddressDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AddressDB.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(new Address()
            {
                Id = 1,
                Street = "De Schans",
                City = "Lelystad",
                HouseNumbering = "15 - 21",
                PostalCode = "8231 KJ",
                Country = "Netherlands"
            });
        }

    }
}
