namespace Bookstore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthday { get; set; } = new DateOnly(1990, 4, 28);

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
