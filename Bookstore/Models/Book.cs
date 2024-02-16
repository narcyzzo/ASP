namespace Bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly Publication { get; set; }
        public Genre Genre { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }

    public enum Genre
    {
        Thriller = 1,
        Action = 2,
        Drama = 3,
        Horror = 4,
        Romance = 5,
        ScienceFiction = 6,
        Fantasy = 7,
        Biography = 8
    }
}
