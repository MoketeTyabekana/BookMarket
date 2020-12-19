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
        //POST Index Action Method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var books = _db.Books.Include(c => c.ProductTypes).Include(c => c.SpecialTagName)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if(lowAmount==null || largeAmount == null)
            {
                 books = _db.Books.Include(c => c.ProductTypes).Include(c => c.SpecialTagName).ToList();

            }
            return View(books);
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
                var searchBook = _db.Books.FirstOrDefault(c => c.Title == books.Title);
                if (searchBook != null)
                {
                    ViewBag.message = "This product already exists!";

                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["SpecialTagId"] = new SelectList(_db.SpecialTagNames.ToList(), "Id", "SpecialTagName");
                    return View();
                }
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
            if(id==null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.ProductTypes).Include(c => c.SpecialTagName)
                .FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        //POST Edit Action Method
        public async Task<IActionResult> Edit(Books books, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    books.Image = "images/" + image.FileName;
                }
                _db.Books.Update(books);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);

        }

        //Get Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.ProductTypes).Include(c => c.SpecialTagName)
                .FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //Get Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _db.Books.Include(c => c.SpecialTagName).Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //Post Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _db.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}