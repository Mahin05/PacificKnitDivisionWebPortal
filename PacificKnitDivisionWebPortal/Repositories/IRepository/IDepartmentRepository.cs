using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task Update(Department obj);
    }
}
