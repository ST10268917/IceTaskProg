using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Data;
using MovieTicketingSystem.Models;

namespace MovieTicketingSystem.Controllers
{
    public class TicketModelsController : Controller
    {
        private readonly MovieTicketingSystemContext _context;

        public TicketModelsController(MovieTicketingSystemContext context)
        {
            _context = context;
        }

        // GET: TicketModels
        public async Task<IActionResult> Index()
        {
            var movieTicketingSystemContext = _context.TicketModel.Include(t => t.Showtime);
            return View(await movieTicketingSystemContext.ToListAsync());
        }

        // GET: TicketModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketModel = await _context.TicketModel
                .Include(t => t.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketModel == null)
            {
                return NotFound();
            }

            return View(ticketModel);
        }

        // GET: TicketModels/Create
        public IActionResult Book()
        {
            ViewData["ShowtimeId"] = new SelectList(_context.ShowtimeModel, "Id", "Id");
            return View();
        }

        // POST: TicketModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book([Bind("Id,ShowtimeId,CustomerName,NumberOfTickets")] TicketModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShowtimeId"] = new SelectList(_context.ShowtimeModel, "Id", "Id", ticketModel.ShowtimeId);
            return View(ticketModel);
        }

        // GET: TicketModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketModel = await _context.TicketModel.FindAsync(id);
            if (ticketModel == null)
            {
                return NotFound();
            }
            ViewData["ShowtimeId"] = new SelectList(_context.ShowtimeModel, "Id", "Id", ticketModel.ShowtimeId);
            return View(ticketModel);
        }

        // POST: TicketModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowtimeId,CustomerName,NumberOfTickets")] TicketModel ticketModel)
        {
            if (id != ticketModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketModelExists(ticketModel.Id))
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
            ViewData["ShowtimeId"] = new SelectList(_context.ShowtimeModel, "Id", "Id", ticketModel.ShowtimeId);
            return View(ticketModel);
        }

        // GET: TicketModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketModel = await _context.TicketModel
                .Include(t => t.Showtime)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketModel == null)
            {
                return NotFound();
            }

            return View(ticketModel);
        }

        // POST: TicketModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketModel = await _context.TicketModel.FindAsync(id);
            if (ticketModel != null)
            {
                _context.TicketModel.Remove(ticketModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketModelExists(int id)
        {
            return _context.TicketModel.Any(e => e.Id == id);
        }
    }
}
