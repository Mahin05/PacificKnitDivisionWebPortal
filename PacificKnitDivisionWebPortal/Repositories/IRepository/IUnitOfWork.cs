using OnlineBookOrderManagementSystem.Repositories.Repository;

namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        //IApplicationUserReposiory applicationUser { get; }
        IDocumentRepository Document { get; }
        IDepartmentRepository department { get; }
        IIPPhoneDetailsRepository iPPhoneDetails { get; }
        void Save();
    }
}
