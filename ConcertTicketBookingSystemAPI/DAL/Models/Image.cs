using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Image
    {
        [Required]
        public int ImageId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public byte[] Source { get; set; }
    }
}
