using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Index(string? searchQuery, string? sortOrder, int page = 1)
        {
            int pageSize = 10; // Number of records to display per page
            IQueryable<Anime> animes = _context.Animes;

            // Apply search query if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                animes = animes.Where(a => a.Name.Contains(searchQuery)); // Filter by search query
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "Episodes":
                    animes = animes.OrderBy(a => a.Episodes);
                    break;
                case "Aired":
                    animes = animes.OrderBy(a => a.Aired);
                    break;
                case "AddedBy":
                    animes = animes.OrderBy(a => a.AddedBy);
                    break;
                case "Name":
                default:
                    animes = animes.OrderBy(a => a.Name);
                    break;
            }

            int totalAnimes = await animes.CountAsync();
            int totalPages = (int)Math.Ceiling(totalAnimes / (double)pageSize);

            // Apply pagination
            animes = animes.Skip((page - 1) * pageSize).Take(pageSize);

            var viewModel = new AnimeIndexViewModel
            {
                Animes = await animes.ToListAsync(),
                SearchQuery = searchQuery,
                TotalPages = totalPages,
                CurrentPage = page
            };

            return View(viewModel);
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
            string currentUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy.ToLower() != currentUsername && !User.IsInRole("Admin")) return Forbid();

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

            string currentUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy.ToLower() != currentUsername && !User.IsInRole("Admin")) return Forbid();

            // Update properties safely
            anime.Name = vm.Name;
            anime.Description = vm.Description;
            anime.Episodes = vm.Episodes;
            anime.Aired = vm.Aired;

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

            string currentUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy.ToLower() != currentUsername && !User.IsInRole("Admin")) return Forbid();

            return View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var anime = await _context.Animes.FindAsync(id);
            string currentUsername = User.Identity.Name.Split('@')[0];
            if (anime.AddedBy.ToLower() != currentUsername && !User.IsInRole("Admin")) return Forbid();

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
