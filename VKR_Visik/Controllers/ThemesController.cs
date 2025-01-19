using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VKR_Visik;
using VKR_Visik.Classes;

namespace VKR_Visik.Controllers
{
    public class ThemesController : Controller
    {
        private readonly ApplicationDdContext _context;

        public ThemesController(ApplicationDdContext context)
        {
            _context = context;
        }

        // GET: Themes
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDdContext = _context.Themes.Include(t => t.Sections).Where(t => t.sectionsId ==  id);
            return View(await applicationDdContext.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themes = await _context.Themes
                .Include(t => t.Sections)
                .FirstOrDefaultAsync(m => m.themes_id == id);
            if (themes == null)
            {
                return NotFound();
            }

            return View(themes);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            ViewData["sectionsId"] = new SelectList(_context.Sections, "sections_Id", "sections_Id");
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("themes_id,themes_name,themes_data,sectionsId")] Themes themes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(themes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sectionsId"] = new SelectList(_context.Sections, "sections_Id", "sections_Id", themes.sectionsId);
            return View(themes);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themes = await _context.Themes.FindAsync(id);
            if (themes == null)
            {
                return NotFound();
            }
            ViewData["sectionsId"] = new SelectList(_context.Sections, "sections_Id", "sections_Id", themes.sectionsId);
            return View(themes);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("themes_id,themes_name,themes_data,sectionsId")] Themes themes)
        {
            if (id != themes.themes_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemesExists(themes.themes_id))
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
            ViewData["sectionsId"] = new SelectList(_context.Sections, "sections_Id", "sections_Id", themes.sectionsId);
            return View(themes);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themes = await _context.Themes
                .Include(t => t.Sections)
                .FirstOrDefaultAsync(m => m.themes_id == id);
            if (themes == null)
            {
                return NotFound();
            }

            return View(themes);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themes = await _context.Themes.FindAsync(id);
            if (themes != null)
            {
                _context.Themes.Remove(themes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemesExists(int id)
        {
            return _context.Themes.Any(e => e.themes_id == id);
        }
    }
}
