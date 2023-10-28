using Domain.Entities;
using DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly GameGatherDbContext _context;

        public UserRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            // Add user to database
            _context.Add(user);
            _context.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users
                .Include(user => user.FoodAndDrinksPreference) // Include the preferences
                .FirstOrDefault(user => user.Email == email);
        }

        public void UpdateUser(User user)
        {
            // Update user
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
