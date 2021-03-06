using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {   
        List<Author> authors;

        public AuthorRepository()
        {
          authors = new List<Author>()
          {
              new Author
              {
                  Id=1,FullName="Maro Jaffel"
              },
              new Author
              {
                  Id=2,FullName="Ahmed Jaffel"
              },
              new Author
              {
                  Id=3,FullName="Marwen Jaffel"
              }
          };
        }

        public void Add(Author author)
        {
            author.Id = authors.Max(a => a.Id)+1;
            authors.Add(author);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(Author newAuthor)
        {
            var author = Find(newAuthor.Id);

            author.FullName = newAuthor.FullName;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a=>a.FullName.Contains(term)).ToList();
        }
    }
}