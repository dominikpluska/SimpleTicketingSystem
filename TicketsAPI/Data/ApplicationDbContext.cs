using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
