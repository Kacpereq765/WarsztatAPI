using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warsztat.Data;
using WarsztatAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WarsztatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly warsztatDbContext _context;

        public WorkersController(warsztatDbContext context)
        {
            _context = context;
        }

        // GET: api/workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            try
            {
                var workers = await _context.Workers.ToListAsync();
                return Ok(workers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching workers: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return worker;
        }

        // POST: api/workers
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        {
            if (worker == null)
            {
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorker), new { id = worker.WorkerID }, worker);
        }

        // DELETE: api/workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound(); // Jeśli pracownik nie został znaleziony, zwróć 404
            }

            _context.Workers.Remove(worker); // Usunięcie pracownika z kontekstu
            await _context.SaveChangesAsync(); // Zapisanie zmian w bazie danych

            return NoContent(); // Zwrócenie 204 No Content po udanym usunięciu
        }
    }
}
