using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificKnitDivisionWebPortal.Models
{
    public class IPPhoneDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="IP No")]
        public string IPNo { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }


        [ForeignKey("Unit")]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
    }
}
