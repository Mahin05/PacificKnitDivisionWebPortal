using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class IPPhoneDetailsRepository : Repository<IPPhoneDetails>, IIPPhoneDetailsRepository
    {
        private readonly ApplicationDBContext _db;
        public IPPhoneDetailsRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(IPPhoneDetails entity)
        {

            _db.ipphoneDetails.Update(entity);
        }

    }
}
