using System;
using System.Collections.Generic;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController:Controller
    {   private static int authorCount;
        private readonly IBookstoreRepository<Author> authorRepository;
        public AuthorController(IBookstoreRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public ActionResult Index()
        {
            var authors = authorRepository.List();
            return View(authors);
        }

        public ActionResult Details(int id)
        {   var author = authorRepository.Find(id);
            return View(author);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Author author)
        {
            try
            {   author.Id = ++authorCount;
                authorRepository.Add(author);
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