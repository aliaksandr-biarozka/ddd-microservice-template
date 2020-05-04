using System.Threading.Tasks;

namespace Template.Application.SeedWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
