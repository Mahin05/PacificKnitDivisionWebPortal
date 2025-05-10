using System.Reflection.Metadata;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IDocumentRepository : IRepository<DocumentModel>
    {
        Task Update(DocumentModel obj);
    }
}
