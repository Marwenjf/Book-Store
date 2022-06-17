using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {   
        List<Book> books;

        public BookRepository()
        {
          books = new List<Book>()
          {
              new Book
              {
                  Id=1,Title="C# Programming",Description="No description",
                  ImageUrl = "img3.jpg",
                  Author = new Author{Id=3,FullName="Marwen Jaffel"}
              },
              new Book
              {
                  Id=2,Title="Java Programming",Description="Nothing",
                  ImageUrl = "img1.jpg",
                  Author = new Author{Id=2,FullName="Ahmed Jaffel"}
              },
              new Book
              {
                  Id=3,Title="Python Programming",Description="No data",
                  ImageUrl = "img2.jpg",
                  Author = new Author{Id=2,FullName="Ahmed Jaffel"}
              }
          };
        }

        public void Add(Book book)
        {
            book.Id = books.Max(b => b.Id)+1;
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

        public void Update(Book newBook)
        {
            var book = Find(newBook.Id);

            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }
    }
}