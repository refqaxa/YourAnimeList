using System;
using System.ComponentModel.DataAnnotations;

namespace YourAnimeList.ViewModels
{
    public class AnimeViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Episodes are required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Episodes must be greater than 0.")]
        public int Episodes { get; set; }

        [Required(ErrorMessage = "Aired date is required.")]
        [DataType(DataType.Date)]
        public DateTime Aired { get; set; }
    }
}
