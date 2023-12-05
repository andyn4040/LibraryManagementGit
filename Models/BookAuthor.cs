namespace Library_Management_System.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
