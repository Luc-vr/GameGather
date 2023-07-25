using Domain.Entities;

namespace DomainServices
{
    public interface IUserRepository
    {
        // Create user
        void CreateUser(User user);

        // Get user by email
        User? GetUserByEmail(string email);

        // Update user
        void UpdateUser(User user);

    }
}
