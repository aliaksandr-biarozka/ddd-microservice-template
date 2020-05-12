using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Application.SeedWork;
using Template.Domain;

namespace Template.Infrastructure
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventDispatcher _dispatcher;
        
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(IDomainEventDispatcher dispatcher) => _dispatcher = dispatcher;

        async Task IUnitOfWork.Commit()
        {
            var transaction = await Database.BeginTransactionAsync();

            await SaveChangesAsync();

            await _dispatcher.DispatchDomainEvents(this);

            await transaction.CommitAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
