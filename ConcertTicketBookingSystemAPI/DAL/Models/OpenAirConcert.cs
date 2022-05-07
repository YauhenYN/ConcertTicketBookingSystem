using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("OpenAirConcerts")]
    public class OpenAirConcert : Concert 
    {
        [Required]
        public string Route { get; set; }
        [Required]
        public string HeadLiner { get; set; }
    }
}
