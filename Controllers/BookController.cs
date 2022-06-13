using System;
using System.Collections.Generic;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController:Controller
    {   private static int bookCount;
        private readonly IBookstoreRepository<Book> bookRepository;
        public BookController(IBookstoreRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
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
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {   book.Id = ++bookCount;
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id,IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id,IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }


    }       
}