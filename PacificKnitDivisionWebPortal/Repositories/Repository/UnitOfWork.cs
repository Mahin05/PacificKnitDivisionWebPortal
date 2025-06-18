using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _db;
        //public IApplicationUserReposiory applicationUser { get; private set; }
        public IDocumentRepository Document { get; private set; }
        public IDepartmentRepository department { get; private set; }
        public IDesignationRepository designation { get; private set; }
        public IMailAddressRepository mailAddress { get; private set; }
        public IIPPhoneDetailsRepository iPPhoneDetails { get; private set; }
        public IUnitRepository unit { get; private set; }

        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            //applicationUser = new ApplicationUserReposiory(_db);
            Document= new DocumentRepository(_db);
            department= new DepartmentRepository(_db);
            designation = new DesignationRepository(_db);
            mailAddress = new MailAddressRepository(_db);
            iPPhoneDetails = new IPPhoneDetailsRepository(_db);
            unit = new UnitRepository(_db);
        }

        //public async Task Save()
        //{
        //     await _db.SaveChangesAsync();
        //}
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
