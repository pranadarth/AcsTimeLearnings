using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class MenuFormTypesRepository : IMenuFormTypesRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<MenuFormTypesRepository> _logger;

        public MenuFormTypesRepository(ILogger<MenuFormTypesRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<MenuFormTypeEntity>> GetMenuFormTypes()
        {
            return await _athenaDbcontext.MenuFormTypeEntity.Where(m => m.ActiveStatus == true).ToListAsync();
        }
    }
}
