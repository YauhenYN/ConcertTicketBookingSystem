using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("PartyConcerts")]
    public class PartyConcert : Concert
    {
        [Required]
        public int Censure { get; set; }
    }
}
