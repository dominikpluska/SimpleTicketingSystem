using AuthAPI.Dto;
using DataAccess.Models;
using DataAccess.StaticData;
using GlobalServices;
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
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<JWT> JWTs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Name = "Default",
                LastName = "Administrator",
                Position = "Application Administrator",
                Email = "administrator@default.com"  
                //To be updated! Give user in the SPA an option to change it
            });

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
                GroupName = Configuration.GetDefaultAssigmentGroup()
            });

            modelBuilder.Entity<Group>().HasData(new Group
            {
                GroupId = 2,
                GroupName = "DefaultUsers"
            });

            modelBuilder.Entity<Group>().HasData(new Group
            {
                GroupId = 3,
                GroupName = "SystemAdministrators"
            });

            modelBuilder.Entity<UserAccount>().HasData(new UserAccount
            {
                UserAccountId = 1,
                UserId = 1,
                UserName = "DefaultAdministrator",
                GroupId = 3,
                RoleId = 3,
                IsActive = true,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("DefaultPassword123!")
            });
        }
    }
}
