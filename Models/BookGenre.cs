namespace Library_Management_System.Models
{
    public class BookGenre
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
