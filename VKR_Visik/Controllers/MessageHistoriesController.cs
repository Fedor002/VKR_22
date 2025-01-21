using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VKR_Visik;
using VKR_Visik.Classes;

namespace VKR_Visik.Controllers
{
    public class MessageHistoriesController : Controller
    {
        private readonly ApplicationDdContext _context;

        public MessageHistoriesController(ApplicationDdContext context)
        {
            _context = context;
        }

        // GET: MessageHistories
        public async Task<IActionResult> Index()
        {
            var applicationDdContext = _context.MessageHistory.Include(m => m.Themes).Include(m => m.Users);
            return View(await applicationDdContext.ToListAsync());
        }

        // GET: MessageHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageHistory = await _context.MessageHistory
                .Include(m => m.Themes)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.MH_id == id);
            if (messageHistory == null)
            {
                return NotFound();
            }

            return View(messageHistory);
        }

        // GET: MessageHistories/Create
        public IActionResult Create()
        {
            ViewData["MH_theme"] = new SelectList(_context.Themes, "themes_id", "themes_id");
            ViewData["MH_who"] = new SelectList(_context.Users, "users_Id", "users_Password");
            return View();
        }

        // POST: MessageHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MH_id,MH_who,MH_theme,MH_placemant,MH_answer,MH_data")] MessageHistory messageHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MH_theme"] = new SelectList(_context.Themes, "themes_id", "themes_id", messageHistory.MH_theme);
            ViewData["MH_who"] = new SelectList(_context.Users, "users_Id", "users_Password", messageHistory.MH_who);
            return View(messageHistory);
        }

        // GET: MessageHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageHistory = await _context.MessageHistory.FindAsync(id);
            if (messageHistory == null)
            {
                return NotFound();
            }
            ViewData["MH_theme"] = new SelectList(_context.Themes, "themes_id", "themes_id", messageHistory.MH_theme);
            ViewData["MH_who"] = new SelectList(_context.Users, "users_Id", "users_Password", messageHistory.MH_who);
            return View(messageHistory);
        }

        // POST: MessageHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MH_id,MH_who,MH_theme,MH_placemant,MH_answer,MH_data")] MessageHistory messageHistory)
        {
            if (id != messageHistory.MH_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageHistoryExists(messageHistory.MH_id))
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
            ViewData["MH_theme"] = new SelectList(_context.Themes, "themes_id", "themes_id", messageHistory.MH_theme);
            ViewData["MH_who"] = new SelectList(_context.Users, "users_Id", "users_Password", messageHistory.MH_who);
            return View(messageHistory);
        }

        // GET: MessageHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageHistory = await _context.MessageHistory
                .Include(m => m.Themes)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.MH_id == id);
            if (messageHistory == null)
            {
                return NotFound();
            }

            return View(messageHistory);
        }

        // POST: MessageHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageHistory = await _context.MessageHistory.FindAsync(id);
            if (messageHistory != null)
            {
                _context.MessageHistory.Remove(messageHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageHistoryExists(int id)
        {
            return _context.MessageHistory.Any(e => e.MH_id == id);
        }
    }
}
