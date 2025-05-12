using System.ComponentModel.DataAnnotations;

namespace PacificKnitDivisionWebPortal.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
