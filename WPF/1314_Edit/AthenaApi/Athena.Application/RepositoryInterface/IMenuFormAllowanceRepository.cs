using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IMenuFormAllowanceRepository
    {
        public Task<MenuFormAllowanceEntity> GetMenuFormAllowance(int mfmcSk);
        public Task<int> AddMenuFormAllowance(int allowance, int mfmcSk, bool activeStatus, string userId);
        public Task<bool> UpdateMenuFormAllowance(int allowance, int mfmcSk, bool activeStatus, string userId);
    }
}
