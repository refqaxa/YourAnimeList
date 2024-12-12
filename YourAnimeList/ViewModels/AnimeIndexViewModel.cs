using YourAnimeList.Models;

namespace YourAnimeList.ViewModels
{
    public class AnimeIndexViewModel
    {
        public IEnumerable<Anime> Animes { get; set; }
        public string? SearchQuery { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
