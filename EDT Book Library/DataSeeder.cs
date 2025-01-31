using EDT_Book_Library.Models;

namespace EDT_Book_Library
{
    public class DataSeeder
    {
        private BookLibraryDBContext _db;
        public DataSeeder(BookLibraryDBContext db)
        {
            _db = db;
        }

        public async Task SeedData()
        {
            _db.Books.AddRange(GetTestBooks());
            _db.Customers.AddRange(GetTestCustomers());
            await _db.SaveChangesAsync();
        }

        private List<Book> GetTestBooks()
        {
            List<Book> books = new List<Book>();

            books.Add(new Book { Name = "Harry Potter", Author = "JK", Description = "You are a wizard Harry", IsAvailable = true });
            books.Add(new Book { Name = "Lord of the Rings", Author = "Tolkein", Description = "A story about taking some hobbits to Isengard", IsAvailable = true });

            return books;
        }
        private List<Customer> GetTestCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Name = "John Smith", Email = "John.smith@email.com", Phone = "01234 566421" });
            customers.Add(new Customer { Name = "Jeremy Irons", Email = "drhouse@house.com", Phone = "07735 366234" });

            return customers;
        }
    }
}
