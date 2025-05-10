namespace OnlineBookOrderManagementSystem.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        //IApplicationUserReposiory applicationUser { get; }
        IDocumentRepository Document { get; }
        void Save();
    }
}
