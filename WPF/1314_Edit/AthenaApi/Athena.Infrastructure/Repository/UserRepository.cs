using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public object GetById(int userId)
        {
            return 1;
        }

        public async Task<User> Login(string userId, string encPassword, string application)
        {
            User user = await _athenaDbcontext.User.Where(u => u.UserId == userId && u.Password == encPassword).Select(u => u).SingleOrDefaultAsync();

            return user;
        }

        public async Task<bool> AddLoginTrackerForUserId(string userId)
        {
            int userSk=await _athenaDbcontext.User.Where(u=>u.UserId == userId).Select(u=>u.UserSk).FirstOrDefaultAsync();

            LoginTracker newLogin = new LoginTracker()
            {
                LoginDate = DateTime.UtcNow,
                UserId = userId,
                UserSk= userSk
            };

            await _athenaDbcontext.LoginTracker.AddAsync(newLogin);
            await _athenaDbcontext.SaveChangesAsync();
            return true;
        }
    }
}
