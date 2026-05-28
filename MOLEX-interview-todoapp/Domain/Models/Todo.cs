using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOLEX_interview_todoapp.Domain.Models
{
    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool Completed { get; set; } = false;
    }
}
