using System;

namespace Library_Management_System.Models
{
    public class Author
    {
        public Author()
        {
            FirstName = "";
            LastName = "";
        }

        /// <summary>
        /// Hidden author id auto-generated
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// First name of author
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of author
        /// </summary>
        public string LastName { get; set; }
    }
}
