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

namespace YourAnimeList.Controllers
{
    public class UserAnimeListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAnimeListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAnimeLists
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the correct GUID UserId from the claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Forbid(); // Handle invalid user scenario

                //var userId = User.Identity.Name;
                var userAnimeList = await _context.UserAnimeLists
                    .Include(x => x.Anime)
                    //.Include(u => u.User)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                return View(userAnimeList);
            }
            return RedirectToAction("Login", "Account");
        }

        // UserAnimeLists/Add/ Add an anime to the user's list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid animeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Forbid();

                //var userId = User.Identity.Name;
                var exists = await _context.UserAnimeLists
                    .AnyAsync(x => x.AnimeId == animeId && x.UserId == userId);

                if (!exists)
                {
                    var userAnime = new UserAnimeList
                    {
                        Id = Guid.NewGuid(),
                        AnimeId = animeId,
                        UserId = userId,
                        AddedOn = DateTime.Now
                    };

                    _context.UserAnimeLists.Add(userAnime);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return Forbid();
        }

        // DELETE: UserAnimeLists/Delete/{animeId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid animeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get the logged-in user's ID from the claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Forbid();

                // Find the anime in the user's list
                var userAnime = await _context.UserAnimeLists
                    .FirstOrDefaultAsync(x => x.AnimeId == animeId && x.UserId == userId);

                if (userAnime != null)
                {
                    _context.UserAnimeLists.Remove(userAnime);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Details", "Animes", new { id = animeId });
            }
            return Forbid();
        }


    }
}
