using System;

namespace Library_Management_System.Models
{
	public class User
	{
		public User()
		{
			FirstName = "";
			LastName = "";
			Email = "";
		}

        /// <summary>
        /// User id auto-generated
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Type of user (borrower or admin)
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of user
        /// </summary>
		public string LastName { get; set; }

        /// <summary>
        /// Email of user
        /// </summary>
        public string Email { get; set; }
    }

    public enum UserType 
	{ 
		Admin,
		Borrower
	}
}
