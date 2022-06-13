using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {   
        List<Book> books;

        public BookRepository()
        {
          books = new List<Book>();
          /*{
              new Book
              {
                  Id=1,Title="C# Programming",Description="No description"
              },
              new Book
              {
                  Id=2,Title="Java Programming",Description="Nothing"
              },
              new Book
              {
                  Id=3,Title="Python Programming",Description="No data"
              }
          };*/
        }

        public void Add(Book book)
        {
            books.Add(book);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);

            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
        }
    }
}