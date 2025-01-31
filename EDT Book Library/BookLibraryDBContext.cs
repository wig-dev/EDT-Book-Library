using EDT_Book_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace EDT_Book_Library
{
    public class BookLibraryDBContext : DbContext
    {
        public BookLibraryDBContext(DbContextOptions<BookLibraryDBContext> options) : base(options) { }


        public override int SaveChanges()
        {
            UpdateBaseEntity();
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            UpdateBaseEntity();
            return await base.SaveChangesAsync();
        }

        private void UpdateBaseEntity()
        {
            var now = DateTime.UtcNow;
            foreach(var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is IEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            entity.ModifiedDate = now;
                            break;
                        case EntityState.Modified:
                            entity.ModifiedDate = now;
                            break;
                    }
                }
            }
        }


        public DbSet<Book> Books => Set<Book>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BookCheckout> BookCheckout => Set<BookCheckout>();

    }
}
