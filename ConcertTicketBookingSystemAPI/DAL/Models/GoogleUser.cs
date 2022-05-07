using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("GoogleUsers")]
    public class GoogleUser : User
    {
        [Required]
        public string GoogleId { get; set; }
    }
}
