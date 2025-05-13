using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IIPPhoneDetailsRepository : IRepository<IPPhoneDetails>
    {
        Task Update(IPPhoneDetails obj);
    }
}
