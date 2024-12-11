using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourAnimeList.Data;
using YourAnimeList.Models;
using YourAnimeList.ViewModels;

namespace YourAnimeList.Controllers
{
    public class AnimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Animes.ToListAsync());
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var anime = await _context.Animes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anime == null) return NotFound();

            // Chech if the logged user have the anime in their list
            bool isInUserList = false;
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                isInUserList = await _context.UserAnimeLists
                    .AnyAsync(x => x.AnimeId == id && x.UserId == userId);
            }
            ViewBag.IsInUserList = isInUserList;

            return View(anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            if (User.Identity == null) return Forbid();

            return View();
        }

        // POST: Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimeViewModel animeModel)
        {
            if (ModelState.IsValid)
            {
                Anime anime = new Anime()
                {
                    Name  = animeModel.Name,
                    Description = animeModel.Description,
                    Episodes = animeModel.Episodes,
                    Aired = animeModel.Aired,
                    AddedBy = User.Identity.Name.Split('@')[0] // Shortened username
                };

                _context.Add(anime);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = anime.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Animes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            // check if item exists
            if (id == null) return NotFound();

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null) return NotFound();

            // Prevent unauthorized edits
            string rightUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy != rightUsername && User.IsInRole("Admin")) return Forbid();

            var vm = new AnimeViewModel()
            {
                Id = anime.Id,
                Name = anime.Name,
                Description = anime.Description,
                Episodes = anime.Episodes,
                Aired = anime.Aired
            };

            return View(vm);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnimeViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            // Preserve form data on validation failure
            if (!ModelState.IsValid) return View(vm);

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null) return NotFound();

            string rightUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy != rightUsername && !User.IsInRole("Admin")) return Forbid();

            // Update properties safely
            anime.Name = anime.Name;
            anime.Description = anime.Description;
            anime.Episodes = anime.Episodes;
            anime.Aired = anime.Aired;

            try
            {
                _context.Update(anime);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeExists(anime.Id)) return NotFound();
                else throw;
            }

            return RedirectToAction("Details", new { id = anime.Id });
        }


        // GET: Animes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var anime = await _context.Animes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anime == null) return NotFound();

            string rightUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy != rightUsername && !User.IsInRole("Admin")) return Forbid(); 

            return View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var anime = await _context.Animes.FindAsync(id);
            string rightUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy != rightUsername && !User.IsInRole("Admin")) return Forbid();

            if (anime != null) _context.Animes.Remove(anime);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(Guid id)
        {
            return _context.Animes.Any(e => e.Id == id);
        }
    }
}
