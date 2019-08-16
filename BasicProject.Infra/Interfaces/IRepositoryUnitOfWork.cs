using BasicProject.Domain.Interface;

namespace BasicProject.Infra.Interfaces
{
    public interface IRepositoryUnitOfWork
    {
        IRepositoryUser Users { get; }

        IRepositoryAddress Addresses { get; }

        IRepositoryLog Logs { get; }

        bool Commit();
    }
}
