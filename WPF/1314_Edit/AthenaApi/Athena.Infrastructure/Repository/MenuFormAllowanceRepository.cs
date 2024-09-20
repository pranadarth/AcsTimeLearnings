using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class MenuFormAllowanceRepository : IMenuFormAllowanceRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<MenuFormAllowanceRepository> _logger;

        public MenuFormAllowanceRepository(ILogger<MenuFormAllowanceRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<int> AddMenuFormAllowance(int allowance, int mfmcSk, bool activeStatus, string userId)
        {
            MenuFormAllowanceEntity menuFormAllowanceEntity = new MenuFormAllowanceEntity
            {
                MenuFormMealCourseSk = mfmcSk,
                AllowanceQty = allowance,
                ActiveStatus = activeStatus,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
            };

            await _athenaDbcontext.MenuFormAllowanceEntity.AddAsync(menuFormAllowanceEntity);
            await _athenaDbcontext.SaveChangesAsync();

            return menuFormAllowanceEntity.MenuFormAllowanceSk;

        }

        public async Task<bool> UpdateMenuFormAllowance(int allowance, int mfmcSk, bool activeStatus, string userId)
        {
            MenuFormAllowanceEntity menuFormAllowanceEntity = await _athenaDbcontext.MenuFormAllowanceEntity
                                                .SingleOrDefaultAsync(x => x.MenuFormMealCourseSk == mfmcSk);

            if (menuFormAllowanceEntity == null)
            {
                return false;
            }
            
            menuFormAllowanceEntity.AllowanceQty = allowance;
            menuFormAllowanceEntity.ActiveStatus = activeStatus;
            menuFormAllowanceEntity.ModifiedBy = userId;
            menuFormAllowanceEntity.ModifiedDate = DateTime.UtcNow;
            _athenaDbcontext.MenuFormAllowanceEntity.Update(menuFormAllowanceEntity);
            await _athenaDbcontext.SaveChangesAsync();

            return true;
        }

            public async Task<MenuFormAllowanceEntity> GetMenuFormAllowance(int mfmcSk)
        {
            MenuFormAllowanceEntity menuFormAllowanceEntity = await _athenaDbcontext.MenuFormAllowanceEntity
                                               .Where(x => x.MenuFormMealCourseSk == mfmcSk).SingleOrDefaultAsync();

            return menuFormAllowanceEntity;

        }
    }
}

