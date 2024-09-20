using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IUserService
    {
        public object GetById(int userId);
        public Task<LoginDetailsModel> Login(string userName, string password, string application);
    }
}
