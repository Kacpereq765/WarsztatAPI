using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarsztatAPI.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; } 

        public bool IsBroken { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int Year { get; set; }
    }

}
