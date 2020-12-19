using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagNamesController : Controller
    {
        private ApplicationDbContext _db;

        public SpecialTagNamesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.SpecialTagNames.ToList());
        }

        //Create Get Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SpecialTagNames specialTagNames)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTagNames.Add(specialTagNames);
                await _db.SaveChangesAsync();
                TempData["save"] = "Category saved";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTagNames);
        }

        //Edit Get Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTagName = _db.SpecialTagNames.Find(id);
            if (specialTagName == null)
            {
                return NotFound();
            }
            return View(specialTagName);
        }

        //Edit Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(SpecialTagNames specialTagNames)
        {
            if (ModelState.IsValid)
            {
                _db.Update(specialTagNames);
                await _db.SaveChangesAsync();
                TempData["update"] = "Category Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTagNames);
        }

        //Details Get Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTagName = _db.SpecialTagNames.Find(id);
            if (specialTagName == null)
            {
                return NotFound();
            }
            return View(specialTagName);
        }

        //Details Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Details(SpecialTagNames specialTagNames)
        {
            return RedirectToAction(nameof(Index));

        }

        //Delete Get Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTagName = _db.SpecialTagNames.Find(id);
            if (specialTagName == null)
            {
                return NotFound();
            }
            return View(specialTagName);
        }

        //Delete Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id, SpecialTagNames specialTagNames)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != specialTagNames.Id)
            {
                return NotFound();
            }

            var specialTagName = _db.SpecialTagNames.Find(id);
            if (specialTagNames == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(specialTagName);
                await _db.SaveChangesAsync();
                TempData["remove"] = "Category deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTagNames);
        }



    }
}