using Store.Domain.Customers;
using Microsoft.EntityFrameworkCore;
namespace Store.Infrastructure.Persistence.Repositories;


public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task AddAsync(Customer customer) => await _dbContext.Customers.AddAsync(customer);


    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);


    public Task UpdateAsync(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Customer customer)
    {
        _dbContext.Customers.Remove(customer);
        return Task.CompletedTask;
    }
}
