using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("FacebookUsers")]
    public class FacebookUser : User
    {
        [Required]
        public string FacebookId { get; set; }
    }
}
