using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Models;

namespace MovieTicketingSystem.Data
{
    public class MovieTicketingSystemContext : DbContext
    {
        public MovieTicketingSystemContext (DbContextOptions<MovieTicketingSystemContext> options)
            : base(options)
        {
        }

        public DbSet<MovieTicketingSystem.Models.MovieModel> MovieModel { get; set; } = default!;
        public DbSet<MovieTicketingSystem.Models.ShowtimeModel> ShowtimeModel { get; set; } = default!;
        public DbSet<MovieTicketingSystem.Models.TicketModel> TicketModel { get; set; } = default!;
    }
}
