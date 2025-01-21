using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VKR_Visik;
using VKR_Visik.Classes;

namespace VKR_Visik.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDdContext _context;

        public UsersController(ApplicationDdContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string? users_FIO, string? users_Password)
        {
            var reslt = await _context.Users.FirstOrDefaultAsync(m => m.users_FIO == users_FIO);

            if (reslt.users_Password.Equals(users_Password) && reslt.Users_AccountActive.Equals(1))
            {
                    HttpContext.Session.SetString("userName", users_FIO);
                    HttpContext.Session.SetString("userRole", reslt.users_Role);
                    HttpContext.Session.SetInt32("userId", reslt.users_Id);
                return RedirectToAction("Index","Sections");
            }
            else
                return View();
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.users_Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("users_Id,users_FIO,users_Password,users_Role,Users_AccountActive")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.Users_AccountActive = 0;
                users.users_Role = "Не присвоен";
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sections");
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("users_Id,users_FIO,users_Password,users_Role,Users_AccountActive")] Users users)
        {
            if (id != users.users_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.users_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.users_Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.users_Id == id);
        }

        public async Task<IActionResult> ViewList()
        {
            var res = _context.Users.Where(m => m.Users_AccountActive == 0);
            return View(await res.ToListAsync());
        }

        public async Task<IActionResult> UserList()
        {
            var res = _context.Users.Where(m => m.Users_AccountActive == 1);
            return View(await res.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _context.Users.FirstOrDefaultAsync(m => m.users_Id == id);

            if (res == null)
            {
                return NotFound();
            }

            res.users_Role = "Менеджер";
            res.Users_AccountActive = 1;

            try
            {
                _context.Update(res);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(res.users_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Sections");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Down(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _context.Users.FirstOrDefaultAsync(m => m.users_Id == id);

            if (res == null)
            {
                return NotFound();
            }

            res.users_Role = "Пользователь";
            res.Users_AccountActive = 1;

            try
            {
                _context.Update(res);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(res.users_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Sections");
        }

        // POST: Users/ViewList/4
        [HttpPost, ActionName("ViewList")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _context.Users.FirstOrDefaultAsync(m => m.users_Id == id);

            if (res == null)
            {
                return NotFound();
            }

            res.users_Role = "Пользователь";
            res.Users_AccountActive = 1;

            try
            {
                _context.Update(res);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(res.users_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Sections"); 
        }

    }
}
