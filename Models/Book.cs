namespace Library_Management_System.Models
{
    public class Book
    {
        public Book()
        {
            Name = "";
            AuthorId = 0;
            ISBNumber = 0;
            Summary = "";
            Available = true;
            Pages = 0;
            GenreId = 0;
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
        /// Author id foreign key
        /// </summary>
        public int AuthorId { get; set; }

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

        /// <summary>
        /// Genre id foreign key
        /// </summary>
        public int GenreId { get; set; }
    }
}