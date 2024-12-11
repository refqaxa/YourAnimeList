namespace YourAnimeList.Models
{
    public class UserAnimeList
    {
        public Guid Id { get; set; }

        // Foreign Key to the Anime model
        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; }

        // Foreign Key to the Application User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
