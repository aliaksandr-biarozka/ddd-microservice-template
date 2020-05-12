using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Domain;

namespace Template.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context) => _context = context;

        public async Task<Order> Get(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task Save(Order order)
        {
            if(order.Id == 0)
            {
                await _context.Orders.AddAsync(order);
            }
            else
            {
                _context.Orders.Update(order);
            }
        }
    }
}
