using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class DocumentRepository : Repository<DocumentModel>, IDocumentRepository
    {
        private readonly ApplicationDBContext _db;
        public DocumentRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(DocumentModel entity)
        {

            var objFromDB = _db.documents.FirstOrDefault(u => u.Id == entity.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = entity.Name;
                objFromDB.DisplayOrder = entity.DisplayOrder;
                objFromDB.FileType = entity.FileType;
                objFromDB.IsDelete = entity.IsDelete;
                if (entity.FileUrl != null)
                {
                    objFromDB.FileUrl = entity.FileUrl;
                }
            }

            _db.documents.Update(entity);
        }

    }
}
