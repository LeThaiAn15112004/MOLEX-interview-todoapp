using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOLEX_interview_todoapp.Domain.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
