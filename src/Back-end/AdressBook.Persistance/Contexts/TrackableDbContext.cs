using AdressBook.Persistance.TrakingEntity.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdressBook.Persistance.Contexts
{
    public abstract class TrackableDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(ITrackEntity).IsAssignableFrom(entityType.ClrType)) continue;

                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyMethod = Expression.Property(parameter, nameof(ITrackEntity.IsDeleted));
                var filter = Expression.Lambda(Expression.Equal(propertyMethod, Expression.Constant(false)), parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

        public override int SaveChanges()
        {
            ApplyTrackableEntityRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTrackableEntityRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyTrackableEntityRules()
        {
            var entries = ChangeTracker.Entries<ITrackEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdateDate = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    };
}
