namespace ProductFlow.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}