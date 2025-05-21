using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificKnitDivisionWebPortal.Models
{
    public class IPPhoneDisplayOrder
    {
        [Key]
        public int Id { get; set; }
        public int DisplayNo { get; set; }
        public string ?Unit { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }
    }
}
