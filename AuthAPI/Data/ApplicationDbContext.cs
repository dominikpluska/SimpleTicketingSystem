using DataAccess.Models;
using DataAccess.StaticData;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AuthAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<JWT> JWTs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 1,
                RoleName = StaticData.StandardRoles.User.ToString(),
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 2,
                RoleName = StaticData.StandardRoles.Agent.ToString(),
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 3,
                RoleName = StaticData.StandardRoles.Administrator.ToString(),
            });

            modelBuilder.Entity<Group>().HasData(new Group
            {
                GroupId = 1,
                GroupName = StaticData.defaultAssingmentGroup.ToString(),
            });
        }
    }
}
