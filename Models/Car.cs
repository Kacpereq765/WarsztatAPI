namespace WarsztatAPI.Models
{
    public class Car
    {
        public int Id { get; set; } 
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsBroken { get; set; }
        public decimal Price { get; set; }
    }
}
