using System;
using System.Collections.Generic;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController:Controller
    {    
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
            {   
                authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            try
            {
                authorRepository.Update(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var author = authorRepository.Find(id);
            return View(author);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {    authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }


    }       
}