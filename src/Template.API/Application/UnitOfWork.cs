namespace Template.API.Application
{
    public interface UnitOfWork
    {
        ITransaction BeginTransaction();
    }
}
