
namespace EDT_Book_Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Author { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }

    }
}
