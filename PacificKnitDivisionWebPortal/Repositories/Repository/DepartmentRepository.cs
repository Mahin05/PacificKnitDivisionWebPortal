using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDBContext _db;
        public DepartmentRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(Department entity)
        {

            _db.departments.Update(entity);
        }

    }
}
