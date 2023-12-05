using System;

namespace Library_Management_System.Models
{
    public class Genre
    {
        public Genre()
        {
            Name = "";
        }

        /// <summary>
        /// Hidden genre id auto-generated
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Name of the genre
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
