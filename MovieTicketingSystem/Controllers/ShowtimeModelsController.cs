using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Data;
using MovieTicketingSystem.Models;

namespace MovieTicketingSystem.Controllers
{
    public class ShowtimeModelsController : Controller
    {
        private readonly MovieTicketingSystemContext _context;

        public ShowtimeModelsController(MovieTicketingSystemContext context)
        {
            _context = context;
        }

        // GET: ShowtimeModels
        public async Task<IActionResult> Index()
        {
            var movieTicketingSystemContext = _context.ShowtimeModel.Include(s => s.Movie);
            return View(await movieTicketingSystemContext.ToListAsync());
        }

        // GET: ShowtimeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtimeModel = await _context.ShowtimeModel
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showtimeModel == null)
            {
                return NotFound();
            }

            return View(showtimeModel);
        }

        // GET: ShowtimeModels/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.MovieModel, "Id", "Title");
            return View();
        }

        // POST: ShowtimeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,ShowTime,Duration,AvailableSeats")] ShowtimeModel showtimeModel)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(showtimeModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception or inspect it for debugging
                    Console.WriteLine($"Error saving data: {ex.Message}");
                    // Optionally, add a user-friendly message to the ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the showtime.");
                }
            }
            else
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }

            // Repopulate the ViewData for the dropdown
            ViewData["MovieId"] = new SelectList(_context.MovieModel, "Id", "Title", showtimeModel.MovieId);

            // Return the view with the model to show errors
            return View(showtimeModel);
        }


        // GET: ShowtimeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtimeModel = await _context.ShowtimeModel.FindAsync(id);
            if (showtimeModel == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.MovieModel, "Id", "Genre", showtimeModel.MovieId);
            return View(showtimeModel);
        }

        // POST: ShowtimeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,ShowTime,Duration,AvailableSeats")] ShowtimeModel showtimeModel)
        {
            if (id != showtimeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showtimeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowtimeModelExists(showtimeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.MovieModel, "Id", "Genre", showtimeModel.MovieId);
            return View(showtimeModel);
        }

        // GET: ShowtimeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtimeModel = await _context.ShowtimeModel
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showtimeModel == null)
            {
                return NotFound();
            }

            return View(showtimeModel);
        }

        // POST: ShowtimeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showtimeModel = await _context.ShowtimeModel.FindAsync(id);
            if (showtimeModel != null)
            {
                _context.ShowtimeModel.Remove(showtimeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeModelExists(int id)
        {
            return _context.ShowtimeModel.Any(e => e.Id == id);
        }
    }
}
