using OnlineBookOrderManagementSystem.Repositories.Repository;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        //IApplicationUserReposiory applicationUser { get; }
        IDocumentRepository Document { get; }
        IDepartmentRepository department { get; }
        IIPPhoneDetailsRepository iPPhoneDetails { get; }
        IUnitRepository unit { get; }
        void Save();
    }
}
