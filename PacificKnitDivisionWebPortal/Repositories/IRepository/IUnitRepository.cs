using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task Update(Unit obj);
    }
}
