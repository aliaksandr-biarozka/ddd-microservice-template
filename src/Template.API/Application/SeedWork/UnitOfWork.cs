namespace Template.API.Application.SeedWork
{
    public interface UnitOfWork
    {
        ITransaction BeginTransaction();
    }
}
