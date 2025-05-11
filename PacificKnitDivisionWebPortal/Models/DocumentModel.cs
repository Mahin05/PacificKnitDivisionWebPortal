using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PacificKnitDivisionWebPortal.Models
{
    public class DocumentModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="SL No.")]
        public int DisplayOrder { get; set; }
        public string FileType { get; set; }
        public bool IsDelete { get; set; }

        [ValidateNever]
        public string FileUrl { get; set; }
    }
}
