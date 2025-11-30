using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{
    public class CateringService
    {
        private readonly CinemaDbContext db;

        public CateringService(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<CateringItem>> GetCateringItemsByTypeAsync(CateringType type)
        {
            return await db.CateringItems
                .Where(ci => ci.Type == type)
                .ToListAsync();
        }

        public async Task<List<CateringItem>> GetAllCateringFoodAsync()
        {
            return await GetCateringItemsByTypeAsync(CateringType.Popcorn);
        }

        public async Task<List<CateringItem>> GetAllCateringDrinksAsync()
        {
            return await GetCateringItemsByTypeAsync(CateringType.Drink);
        }
    }
}
