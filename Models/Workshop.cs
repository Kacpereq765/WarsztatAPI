using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarsztatAPI.Models
{
    public class Workshop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkshopID { get; set; } 

        public string NameWorkshop { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
