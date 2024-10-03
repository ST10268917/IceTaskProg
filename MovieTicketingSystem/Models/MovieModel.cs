using System.ComponentModel.DataAnnotations;

namespace MovieTicketingSystem.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }
        

        [Required]
        public string Genre { get; set; }
        

        [Required]
        [Range(30, 300, ErrorMessage = "Duration must be between 30 and 300 minutes.")]
        public int Duration { get; set; }
        
    }
}
