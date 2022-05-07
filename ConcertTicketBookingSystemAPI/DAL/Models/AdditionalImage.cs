using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class AdditionalImage
    {
        [Required]
        [Key]
        public int ImageId { get; set; }
        public Image Image { get; set; }
        [Required]
        public int ConcertId { get; set; }
    }
}
