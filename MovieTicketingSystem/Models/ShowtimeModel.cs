using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketingSystem.Models
{
    public class ShowtimeModel
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        // Navigation property to MovieModel
        public MovieModel Movie { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime ShowTime { get; set; }


        [Required]
        [Range(30, 300, ErrorMessage = "Duration must be between 30 and 300 minutes.")]
        public int Duration { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Available seats must be between 1 and 100 .")]
        public int AvailableSeats { get; set; }
    }
}
