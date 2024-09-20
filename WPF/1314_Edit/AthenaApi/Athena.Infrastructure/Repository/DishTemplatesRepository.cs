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
    public class DishTemplatesRepository : IDishTemplatesRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishTemplatesRepository> _logger;

        public DishTemplatesRepository(ILogger<DishTemplatesRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishTemplateEntity>> GetDishTemplates()
        {
            return await _athenaDbcontext.DishTemplateEntity.Where(t => t.ActiveStatus == true).ToListAsync();
        }
    }
}
