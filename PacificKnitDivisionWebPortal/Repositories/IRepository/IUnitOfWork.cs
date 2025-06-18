using OnlineBookOrderManagementSystem.Repositories.Repository;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        //IApplicationUserReposiory applicationUser { get; }
        IDocumentRepository Document { get; }
        IDepartmentRepository department { get; }
        IDesignationRepository designation { get; }
        IIPPhoneDetailsRepository iPPhoneDetails { get; }
        IMailAddressRepository mailAddress { get; }
        IUnitRepository unit { get; }
        void Save();
    }
}
