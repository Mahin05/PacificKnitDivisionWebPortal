using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IMailAddressRepository : IRepository<MailAddress>
    {
        Task Update(MailAddress obj);
    }
}
