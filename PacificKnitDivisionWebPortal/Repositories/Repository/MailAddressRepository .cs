using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class MailAddressRepository  : Repository<MailAddress>, IMailAddressRepository
    {
        private readonly ApplicationDBContext _db;
        public MailAddressRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public async Task Update(MailAddress entity)
        {

            _db.mailAddress.Update(entity);
        }

    }
}
