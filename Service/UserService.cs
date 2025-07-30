using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private readonly ChatApplicationDbContext _context;

        public UserService(ChatApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
                throw new Exception("Email đã tồn tại");

            var user = new User
            {
                Email = email,
                PasswordHash = password // Có thể mã hóa ở đây
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
