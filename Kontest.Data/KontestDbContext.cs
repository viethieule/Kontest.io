using Kontest.Model.Entities;
using Kontest.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Kontest.Data
{
    public class KontestDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public KontestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationCategory> OrganizationCategories { get; set; }
        public DbSet<OrganizationRequest> OrganizationRequests { get; set; }
        public DbSet<UserOrganization> UserOrganizations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("ApplicationUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("ApplicationRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("ApplicationUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("ApplicationUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<Guid>>().ToTable("ApplicationUserTokens").HasKey(x => new { x.UserId });
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IAuditable;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreatedDate = DateTime.Now;
                    }
                    changedOrAddedItem.UpdatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KontestDbContext>
        {
            public KontestDbContext CreateDbContext(string[] args)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<KontestDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new KontestDbContext(builder.Options);
            }
        }
    }
}
