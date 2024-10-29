using DataAccess.Models;
using DataAccess.StaticData;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                CategoryName = "Desktops/Laptops"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 2,
                CategoryName = "Mobile Devices"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 3,
                CategoryName = "Printers and Scanners"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 4,
                CategoryName = "Operating Systems"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 5,
                CategoryName = "Applications"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 6,
                CategoryName = "Email"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 7,
                CategoryName = "Connectivity"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 8,
                CategoryName = "Security"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 9,
                CategoryName = "User Accounts"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 10,
                CategoryName = "File and Resource Access"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 11,
                CategoryName = "Other"
            });

        }
    }
}
