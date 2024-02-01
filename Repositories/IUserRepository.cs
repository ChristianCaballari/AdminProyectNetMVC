using AdminProyectos.Models;

namespace AdminProyectos.services
{
    public interface IUserRepository
    {
        Task<User> SearchByEmail(string normalizedEmail);
        Task<int> CreateUser(User user);
    }
}
