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

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Category> categories = new List<Category> { 
                new Category {
                    CategoryId = 1,
                    CategoryName = "Desktops/Laptops"
                },
                new Category {
                    CategoryId = 2,
                    CategoryName = "Mobile Devices"
                },
                new Category {
                    CategoryId = 3,
                    CategoryName = "Printers and Scanners"
                },
                new Category {
                    CategoryId = 4,
                    CategoryName = "Operating Systems"
                },
                new Category {
                    CategoryId = 5,
                    CategoryName = "Applications"
                },
                new Category {
                    CategoryId = 6,
                    CategoryName = "Email"
                },
                new Category {
                    CategoryId = 7,
                    CategoryName = "Connectivity"
                },
                new Category {
                    CategoryId = 8,
                    CategoryName = "Security"
                },
                new Category {
                    CategoryId = 9,
                    CategoryName = "User Accounts"
                },
                new Category {
                    CategoryId = 10,
                    CategoryName = "File and Resource Access"
                },
                new Category {
                    CategoryId = 11,
                    CategoryName = "Other"
                },
            };

            foreach (var category in categories)
            {
                modelBuilder.Entity<Category>().HasData(new Category
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });
            }
        }
    }
}
