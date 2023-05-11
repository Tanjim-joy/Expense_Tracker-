using Expense01.Data;
using Expense01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense01.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext Db;

        public UserController (UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            this.Db = db;
        }
        public IActionResult Index()
        {
            return View(Db.Appusers.ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AppUser appusers)
        {
            if (ModelState.IsValid)
            {
                var res = await _userManager.CreateAsync(appusers, appusers.PasswordHash);
                if (res.Succeeded)
                {
                    TempData["Save"] = "Data Save Successfully";
                    return RedirectToAction("Index");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View();  
        }

        public async Task<IActionResult> Edit(String id)
        {
            var u = Db.Appusers.FirstOrDefault(x=>x.Id == id);
            if(u == null)
            {
                return NotFound();
            }
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppUser appUser)
        {
            var userinfo = Db.Appusers.FirstOrDefault(x=>x.Id == appUser.Id);
            if (userinfo == null)
            {
                return NotFound();
            }
            userinfo.FirstName = appUser.FirstName;
            userinfo.LastName = appUser.LastName;
            userinfo.UserName = appUser.UserName;
            userinfo.Email = appUser.Email;
            userinfo.PasswordHash = appUser.PasswordHash;

            var q = await _userManager.UpdateAsync(userinfo);
            if (q.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Info(String id)
        {
            var u = Db.Appusers.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return NotFound();
            }
            return View(u);
        }

        public async Task<IActionResult> Lock(String id)
        {
            var u = Db.Appusers.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return NotFound();
            }
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> Lock(AppUser app)
        {
            var u = Db.Appusers.FirstOrDefault(x=>x.Id == app.Id);
            if (u == null)
            {
                return NotFound();
            }

            u.LockoutEnd = DateTime.Now.AddDays(99);
            int r = Db.SaveChanges();

            if (r > 0)
            {
                return RedirectToAction("Index");
            }
            return View(u);
        }

        public async Task<IActionResult> Act(String id)
        {
            var u = Db.Appusers.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return NotFound();
            }
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> Act(AppUser app)
        {
            var u = Db.Appusers.FirstOrDefault(x => x.Id == app.Id);
            if (u == null)
            {
                return NotFound();
            }

            u.LockoutEnd = null;
            int r = Db.SaveChanges();

            if (r > 0)
            {
                return RedirectToAction("Index");
            }
            return View(u);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var d = Db.Appusers.FirstOrDefault(x=>x.Id == id);
            if (d == null)
            {
                return NotFound();
            }
            return View(d);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AppUser app)
        {
            var delU = Db.Appusers.FirstOrDefault(x=>x.Id == app.Id);
            if (delU == null)
            {
                return NotFound();
            }

            Db.Appusers.Remove(delU);
            int q = Db.SaveChanges();

            if (q > 0)
            {
                return RedirectToAction("Index");
            }

            return View(delU);
        }
    } 
}
