using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("MicrosoftUsers")]
    public class MicrosoftUser : User 
    {
        [Required]
        public string MicrosoftId { get; set; }
    }
}
