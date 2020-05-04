using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Application.SeedWork;
using Template.Domain;

namespace Template.Infrastructure
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventDispatcher _dispatcher;
        //private readonly string _connectionString;
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(IDomainEventDispatcher dispatcher) => _dispatcher = dispatcher;

        async Task IUnitOfWork.Commit()
        {
            await _dispatcher.DispatchDomainEvents(this);

            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
