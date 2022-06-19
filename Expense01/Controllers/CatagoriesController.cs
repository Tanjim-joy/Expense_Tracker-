using Expense01.Data;
using Expense01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense01.Controllers
{
    [Authorize]
    public class CatagoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CatagoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Count = db.dailyExpense.Count();

            var Total = db.dailyExpense.Select(x => x.Amount).Sum();
            ViewBag.Amount = Total.ToString("0.00");

            var maxx = db.dailyExpense.Max(x => x.Amount);
            ViewBag.M = maxx;

            var avg = db.dailyExpense.Average(x=>x.Amount);
            ViewBag.a = avg;

            var q = db.dailyExpense.Select(w=>w.EName);
            var m = q.Max();
            ViewBag.l = m;

            return View(db.categories.ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories c)
        {
            if (ModelState.IsValid)
            {
                if(db.categories.Any(x=>x.CName.ToLower() == c.CName.ToLower()))
                {
                    ModelState.AddModelError("", "Category name already exits");
                    return View(c);
                }
                db.categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(db.categories.FirstOrDefault(x=>x.CategoriesId == id));
        }

        [HttpPost]
        public IActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var n = db.categories.FirstOrDefault(x=>x.CategoriesId == id);
            if (n == null)
            {
                return NotFound();
            }
            return View(n);
        }

        [HttpPost]
        public IActionResult Delete(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }
    }
}
