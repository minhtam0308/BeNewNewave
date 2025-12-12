using BeNewNewave.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<DetailBorrow> DetailBorrows { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartBook> CartBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.IdAuthor)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<DetailBorrow>()
                .HasOne(b => b.Borrow)
                .WithMany(b => b.DetailBorrow)
                .HasForeignKey(b => b.IdBorrow)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetailBorrow>()
                .HasOne(b => b.Book)
                .WithMany(b => b.DetailBorrows)
                .HasForeignKey(b => b.IdBook)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrow>()
                .HasOne(mt => mt.User)
                .WithMany(u => u.Users)
                .HasForeignKey(mt => mt.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrow>()
                .HasOne(mt => mt.Admin)
                .WithMany(u => u.Admins)
                .HasForeignKey(mt => mt.IdAdmin)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartBook>()
                .HasOne(mt => mt.Cart)
                .WithMany(u => u.CartBooks)
                .HasForeignKey(mt => mt.IdCart)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.CartBooks)
                .HasForeignKey(cb => cb.IdBook)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(cb => cb.User)
                .WithOne(b => b.Cart)
                .HasForeignKey<Cart>(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrow>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<DetailBorrow>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Cart>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<CartBook>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Author>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Borrow>().HasQueryFilter(x => !x.IsDeleted);

        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }

            foreach (var entry in ChangeTracker.Entries()
                 .Where(e => e.State == EntityState.Modified))
            {
                entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }
            foreach (var entry in ChangeTracker.Entries()
                 .Where(e => e.State == EntityState.Modified))
            {
                entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
