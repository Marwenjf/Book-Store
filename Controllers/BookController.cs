using System;
using System.IO;
using System.Collections.Generic;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bookstore.Controllers
{
    public class BookController:Controller
    {    
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookstoreRepository<Book> bookRepository,
        IBookstoreRepository<Author> authorRepository,
        IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        public ActionResult Details(int id)
        {   var book = bookRepository.Find(id);
            return View(book);
        }

        public ActionResult Create()
        {   var model = new BookAuthorViewModel
            {
              Authors = FillSelectList()
            };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {   
            if(ModelState.IsValid)
            {
            try
            {   
                string fileName = UploadFile(model.File) ?? string.Empty;
                
                if (model.AuthorId == -1)
                {
                   ViewBag.Message = "Please select an author from the list!";
                   return View(GetAllAuthors());
                }
                var author = authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                  
                  Title = model.Title,
                  Description = model.Description,
                  Author = author,
                  ImageUrl= fileName
                };
                
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {    
                return View();
            }
            }
                ModelState.AddModelError("", "You have to fill all the required fields!");
                return View(GetAllAuthors());
            
        }

        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                System.Console.WriteLine("viewModel.ImageUrl ===== Edit "+viewModel.ImageUrl);
                // TODO: Add update logic here
                string fileName = UploadFile(viewModel.File, viewModel.ImageUrl);
                 
                var author = authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Id=viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl = fileName
                };
                System.Console.WriteLine("book ===== Edit "+book.ImageUrl);
                bookRepository.Update(book);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {   
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please select an author ---" });

            return authors;
        }

        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return vmodel;
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }

            return null;
        }

        string UploadFile(IFormFile file, string imageUrl)
        {   
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);
                if (oldPath != newPath)
                {
                    System.GC.Collect(); 
                    System.GC.WaitForPendingFinalizers(); 
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            } 
            return imageUrl;
        }

        public ActionResult Search(string term)
        {
            var result = bookRepository.Search(term);

            return View("Index", result);
        }


    }       
}