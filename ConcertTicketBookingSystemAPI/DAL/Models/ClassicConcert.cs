using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("ClassicConcerts")]
    public class ClassicConcert : Concert
    {
        [Required]
        public string VoiceType { get; set; }
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public string Compositor { get; set; }
    }
}
