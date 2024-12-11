namespace YourAnimeList.Models
{
    public class Anime
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Episodes { get; set; }

        public DateTime Aired { get; set; }

        public string AddedBy { get; set; }

        // Navigation property for the many-to-many relationship
        public List<UserAnimeList>? UserAnimeLists { get; set; }
    }
}
