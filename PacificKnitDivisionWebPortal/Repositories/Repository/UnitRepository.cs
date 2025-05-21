using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly ApplicationDBContext _db;
        public UnitRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(Unit entity)
        {

            _db.unit.Update(entity);
        }

    }
}
