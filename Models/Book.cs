namespace Library_Management_System.Models
{
    public class Book
    {
        public Book()
        {
            Name = "";
            ISBNumber = 0;
            Summary = "";
            Available = true;
            Pages = 0;
        }

        /// <summary>
        /// Hidden book id auto-generated
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Name of book
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// International standard book number
        /// </summary>
        public int ISBNumber { get; set; }

        /// <summary>
        /// Brief summary of book
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Availability status
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Count of pages in book
        /// </summary>
        public int Pages { get; set; }
        
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}