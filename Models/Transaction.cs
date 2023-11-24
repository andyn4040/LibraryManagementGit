using System;

namespace Library_Management_System.Models
{
    public class Transaction
    {
        public Transaction()
        {
            UserId = 0;
            BookId = 0;
        }

        /// <summary>
        /// Hidden transaction id auto-generated
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// User id foreign key
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Book id foreign key
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Date the book was borrowed
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Date the book will be returned
        /// </summary>
        public DateTime ReturnDate { get; set; }
    }
}