using System.Threading.Tasks;
using Template.Domain.SeedWork;

namespace Template.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task Save(Order order);

        Task<Order> Get(int orderId);
    }
}
