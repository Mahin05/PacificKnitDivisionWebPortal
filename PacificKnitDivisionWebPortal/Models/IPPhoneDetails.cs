using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificKnitDivisionWebPortal.Models
{
    public class IPPhoneDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IPNo { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Unit { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }
    }
}
