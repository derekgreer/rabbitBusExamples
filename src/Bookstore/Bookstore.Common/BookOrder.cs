namespace Bookstore.Common
{
    public class BookOrder
    {
        public string BookName { get; protected set; }

        public BookOrder(string bookName)
        {
            BookName = bookName;
        }
    }
}