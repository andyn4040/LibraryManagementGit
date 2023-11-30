namespace Library_Management_System.ViewModels
{
    public class TransactionCreateViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime BorrowDate { get; set; }
        public int TransactionId { get; set; }
        /// <summary>
        /// User id foreign key
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Date the book will be returned
        /// </summary>
        public DateTime ReturnDate { get; set; }
    }
}
