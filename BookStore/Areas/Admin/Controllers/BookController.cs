using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;

        public BookController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Books.Include(c=>c.ProductTypes).Include(f=>f.SpecialTagName).ToList());
        }
        //Get Create Method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagId"] = new SelectList(_db.SpecialTagNames.ToList(), "Id", "SpecialTagName");
            return View();
        }

        //Post Create Method
        [HttpPost]

        public async Task<IActionResult>Create(Books books, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if(image!= null)
                {
                    var name = Path.Combine(_he.WebRootPath+"/images",Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    books.Image = "images/" + image.FileName;
                }
                _db.Books.Add(books);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        //Get Edit Action Method
        
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagId"] = new SelectList(_db.SpecialTagNames.ToList(), "Id", "SpecialTagName");
            return View();
        }
    }
}