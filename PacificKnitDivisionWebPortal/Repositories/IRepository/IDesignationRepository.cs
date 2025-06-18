using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IDesignationRepository : IRepository<Designation>
    {
        Task Update(Designation obj);
    }
}
