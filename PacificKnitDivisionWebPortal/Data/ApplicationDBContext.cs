using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PacificKnitDivisionWebPortal.Models;
using PacificKnitDivisionWebPortal.Models.ViewModels;

namespace PacificKnitDivisionWebPortal.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<DocumentModel> documents { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Designation> designation { get; set; }
        public DbSet<IPPhoneDetails> ipphoneDetails { get; set; }
        public DbSet<MailAddress> mailAddress { get; set; }
        public DbSet<Unit> unit { get; set; }




        //public DbSet<IPPhoneDisplayOrder> IPPhoneDisplayOrder { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Ignore<IPPhoneViewModel>();

        //    base.OnModelCreating(modelBuilder); // ✅ Don't forget this if using Identity

        //    modelBuilder.Entity<IPPhoneViewModel>().HasNoKey(); // ✅ Only this
        //}


    }
}
