using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificKnitDivisionWebPortal.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayNo { get; set; }

        [ForeignKey("Unit")]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public Unit ?Unit { get; set; }
    }
}
