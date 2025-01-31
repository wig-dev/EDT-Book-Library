namespace EDT_Book_Library.Models
{
    public class BookCheckout
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public bool IsReturned { get; set; }
        
    }
}
