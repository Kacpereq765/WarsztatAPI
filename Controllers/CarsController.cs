using Microsoft.AspNetCore.Mvc;
using WarsztatAPI.Data;
using Microsoft.EntityFrameworkCore;
using WarsztatAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly warsztatDbContext _context;

    public CarsController(warsztatDbContext context)
    {
        _context = context;
    }

    [HttpGet] // Zmiana na HttpGet
    public async Task<IActionResult> GetCars()
    {
        try
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }
        catch (Exception ex)
        {
            // Logowanie błędu
            Console.WriteLine($"Błąd podczas pobierania samochodów: {ex.Message}");
            return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
        }
    }

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

        return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car); // Zwraca status 201 Created
    }
}
