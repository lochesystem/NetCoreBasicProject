using BasicProject.Domain.Interface;
using BasicProject.Infra.Context;
using BasicProject.Infra.Entity;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Repository.Base;
using System;

namespace BasicProject.Infra.Repository
{
    public class RepositoryUnitOfWork : RepositoryBase<Domain.Model.UnitOfWork, UnitOfWork>, IRepositoryUnitOfWork
    {
        private readonly BasicProjectContext _context;

        private RepositoryUser repositoryUser = null;

        private RepositoryLog repositoryLog = null;

        private RepositoryAddress repositoryAddress = null;

        private bool disposed = false;

        public RepositoryUnitOfWork(BasicProjectContext context) : base(context)
        {
            _context = context;
        }

        public IRepositoryUser Users
        {
            get
            {
                if (repositoryUser == null)
                {
                    repositoryUser = new RepositoryUser(_context);
                }
                return repositoryUser;
            }
        }


        public IRepositoryLog Logs
        {
            get
            {
                if (repositoryLog == null)
                {
                    repositoryLog = new RepositoryLog(_context);
                }
                return repositoryLog;
            }
        }

        public IRepositoryAddress Addresses
        {
            get
            {
                if (repositoryAddress == null)
                {
                    repositoryAddress = new RepositoryAddress(_context);
                }
                return repositoryAddress;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public new bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
