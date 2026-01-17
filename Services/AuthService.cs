using System.Security.Cryptography;
using System.Text;
using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Services
{

    public class AuthService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;

        public async Task<LoginAccount?> LoginAsync(string username, string password)
        {
            var account = await _context.LoginAccounts
                .FirstOrDefaultAsync(a => a.Username == username);

            if (account == null)
                return null;

            if (!VerifyPassword(password, account.PasswordHash))
                return null;

            account.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<LoginAccount> CreateAccountAsync(string username, string password, string name, string surname)
        {
            var exists = await _context.LoginAccounts
                .AnyAsync(a => a.Username == username);

            if (exists)
                throw new InvalidOperationException($"Username '{username}' already exists");

            var account = new LoginAccount
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Name = name,
                Surname = surname
            };

            _context.LoginAccounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> ChangePasswordAsync(int accountId, string oldPassword, string newPassword)
        {
            var account = await _context.LoginAccounts.FindAsync(accountId);
            if (account == null)
                return false;

            if (!VerifyPassword(oldPassword, account.PasswordHash))
                return false;

            account.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        public string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == passwordHash;
        }
    }
}
