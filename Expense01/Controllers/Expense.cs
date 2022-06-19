using Expense01.Data;
using Expense01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense01.Controllers
{
    [Authorize]
    public class Expense : Controller
    {
        private readonly ApplicationDbContext db;
        public Expense(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(DateTime? from, DateTime ? to)
        {
            if (from.HasValue && to.HasValue)
            {
                var data = db.dailyExpense
                    .Include(x => x.Categories)
                    .Where(x => x.EDate.Date >= from.Value.Date && x.EDate <= to.Value.Date)
                    .ToList();
                ViewBag.Count = db.categories.Count();

                var Total = db.dailyExpense.Select(x => x.Amount).Sum();
                ViewBag.Amount = Total.ToString("0.00");

                return View(data);
            }
            else
            {
                var data = db.dailyExpense.Include(x => x.Categories).ToList();

                ViewBag.Count = db.categories.Count();

                var Total = db.dailyExpense.Select(x => x.Amount).Sum();
                ViewBag.Amount = Total;

                var maxx = db.dailyExpense.Max(x => x.Amount);
                ViewBag.M = maxx;

                var avg = db.dailyExpense.Average(x=>x.Amount);
                ViewBag.avg = avg;

                return View(data);
            }
            
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Cat = db.categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DailyExpense expense)
        {
            if (ModelState.IsValid)
            {
                db.dailyExpense.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Cat = db.categories.ToList();
            return View(expense);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var e = db.dailyExpense.FirstOrDefault(x=>x.ExpenseId == id);
            if (e == null)
            {
                return NotFound();
            }
            ViewBag.Cat = db.categories.ToList();
            return View(e);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DailyExpense daily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daily);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var e = db.dailyExpense.FirstOrDefault(x => x.ExpenseId == id);
            if (e == null)
            {
                return NotFound();
            }
            ViewBag.Cat = db.categories.ToList();
            return View(e);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DailyExpense daily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daily);
        }
    }

}
