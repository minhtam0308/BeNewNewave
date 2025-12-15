using BeNewNewave.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Data
{
    public class ImageDBContext(DbContextOptions<ImageDBContext> options) : DbContext(options)
    {
        public DbSet<BookImage> BookImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookImage>().HasQueryFilter(x => !x.IsDeleted);
        }
        //public override int SaveChanges()
        //{
        //    foreach (var entry in ChangeTracker.Entries()
        //             .Where(e => e.State == EntityState.Deleted))
        //    {
        //        entry.State = EntityState.Modified;
        //        entry.CurrentValues["IsDeleted"] = true;
        //    }

        //    return base.SaveChanges();
        //}
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in ChangeTracker.Entries()
        //                 .Where(e => e.State == EntityState.Deleted))
        //    {
        //        entry.State = EntityState.Modified;
        //        entry.CurrentValues["IsDeleted"] = true;
        //    }
        //    foreach (var entry in ChangeTracker.Entries()
        //         .Where(e => e.State == EntityState.Modified))
        //    {
        //        entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}
    }
}
