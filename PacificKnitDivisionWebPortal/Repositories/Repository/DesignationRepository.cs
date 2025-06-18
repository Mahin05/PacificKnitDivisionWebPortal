using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        private readonly ApplicationDBContext _db;
        public DesignationRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(Designation entity)
        {

            _db.designation.Update(entity);
        }

    }
}
