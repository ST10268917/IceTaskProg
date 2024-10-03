using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketingSystem.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Showtime")]
        public int ShowtimeId { get; set; }

        // Navigation property to ShowtimeModel
        public ShowtimeModel Showtime { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        public string CustomerName { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Number of tickets must be between 1 and 10 .")]
        public int NumberOfTickets { get; set; }
    }
}
