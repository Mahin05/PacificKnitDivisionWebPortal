using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificKnitDivisionWebPortal.Models
{
    public class MailAddress
    {
        [Key]
        public int Id { get; set; }
        public int SLNo { get; set; }

        [ForeignKey("Designation")]
        [Display(Name = "Designation")]
        public int DesigId { get; set; }
        public Designation? Designation { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
