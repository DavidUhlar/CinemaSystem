using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class CustomerService
    {
        private readonly CinemaDbContext db;

        public CustomerService(CinemaDbContext db)
        {
            this.db = db;
        }


        public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        {
            db.Customers.Add(newCustomer);
            await db.SaveChangesAsync();
            return newCustomer;
        }

        public async Task UpdateCustomerAsync(Customer updatedCustomer)
        {
            db.Customers.Update(updatedCustomer);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customerToDelete = await db.Customers.FindAsync(id);
            if (customerToDelete != null)
            {
                db.Customers.Remove(customerToDelete);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await db.Customers
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
