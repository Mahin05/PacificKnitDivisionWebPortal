using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PacificKnitDivisionWebPortal.Models;

namespace PacificKnitDivisionWebPortal.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<DocumentModel> documents { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<IPPhoneDetails> ipphoneDetails { get; set; }
    }
}
