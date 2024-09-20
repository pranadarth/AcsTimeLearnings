using Athena.Domain.Entities;

namespace Athena.Application.RepositoryInterface
{
    public interface IUserRepository
    {
        public object GetById(int userId);
        public Task<User> Login(string userName, string password, string application);
        public Task<bool> AddLoginTrackerForUserId(string userId);
    }
}
