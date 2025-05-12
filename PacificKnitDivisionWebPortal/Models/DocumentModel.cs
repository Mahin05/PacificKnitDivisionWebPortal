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
        [Display(Name = "Inactive")]
        public bool IsDelete { get; set; }

        [ValidateNever]
        [Display(Name = "File")]
        public string FileUrl { get; set; }
    }
}
