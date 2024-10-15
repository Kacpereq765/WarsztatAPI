using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warsztat.Data; 
using WarsztatAPI.Models;

namespace WarsztatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly warsztatDbContext _context; 

        public CarsController(warsztatDbContext context) 
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await _context.Cars.ToListAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania samochodów: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        // POST: api/cars
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Zwraca błąd walidacji
            }

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), new { CarID = car.CarID }, car); // Zwraca status 201 Created
        }
    }
}
