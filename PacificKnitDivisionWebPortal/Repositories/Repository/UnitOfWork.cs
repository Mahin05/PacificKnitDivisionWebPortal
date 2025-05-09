﻿using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;

namespace OnlineBookOrderManagementSystem.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _db;
        //public IApplicationUserReposiory applicationUser { get; private set; }
        public IDocumentRepository Document { get; private set; }
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            //applicationUser = new ApplicationUserReposiory(_db);
            Document= new DocumentRepository(_db);
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
