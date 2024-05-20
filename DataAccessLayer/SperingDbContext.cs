using GameApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameApp.DataAccessLayer
{
    public class SperingDbContext : IdentityDbContext<AppUser>
    {
        public SperingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Category> categories { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries().ToList();

            foreach (var entity in entries)
            {
                if(entity.Entity is BaseEntity)
                {
                switch (entity.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)entity.Entity).CreatedTime = DateTime.UtcNow;
                            ((BaseEntity)entity.Entity).IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            ((BaseEntity)entity.Entity).UpdatedTime = DateTime.UtcNow;
                            break;
                    }
                }
                
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
