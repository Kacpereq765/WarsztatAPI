using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warsztat.Data;
using WarsztatAPI.Models;

namespace WarsztatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsController : ControllerBase
    {
        private readonly warsztatDbContext _context;

        public WorkshopsController(warsztatDbContext context)
        {
            _context = context;
        }

        // GET: api/workshops
        [HttpGet]
        public async Task<IActionResult> GetWorkshops()
        {
            try
            {
                var workshops = await _context.Workshops.ToListAsync();
                return Ok(workshops);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania warsztatów: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        // GET: api/workshops/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkshop(int id)
        {
            try
            {
                var workshop = await _context.Workshops.FindAsync(id);
                if (workshop == null)
                {
                    return NotFound();
                }
                return Ok(workshop);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania warsztatu o ID {id}: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        // POST: api/workshops
        [HttpPost]
        public async Task<IActionResult> AddWorkshop([FromBody] Workshop workshop)
        {
            if (workshop == null)
            {
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Zwraca błąd walidacji
            }

            await _context.Workshops.AddAsync(workshop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkshop), new { id = workshop.WorkshopID }, workshop); // Zwraca status 201 Created
        }
    }
}
