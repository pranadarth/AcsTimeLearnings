using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athena.Domain.Models;

namespace Athena.Infrastructure.Repository
{
    public class DishMenuTypeRepository : IDishMenuTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishMenuTypeRepository> _logger;

        public DishMenuTypeRepository(ILogger<DishMenuTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<MenuTypeDetailsModel>> GetDishMenuTypes(int? locationId = null, int? sublocationId = null)
        {
            //return await _athenaDbcontext.DishMenuTypeEntity.Where(x => x.ActiveStatus == true).ToListAsync();

            var initialQuery = from dm in _athenaDbcontext.DishMenuTypeEntity
                               where dm.ActiveStatus == true
                               select new
                               {
                                   DishMenuTypeEntity = dm,
                                   LocationMenuTypeMapping = (LocationMenuTypeMappingEntity)null,
                               };

            if (locationId != null || sublocationId != null)
            {
                initialQuery = from dm in initialQuery
                               join lmtm in _athenaDbcontext.LocationMenuTypeMappingEntity
                               on dm.DishMenuTypeEntity.DishMenuTypeId equals lmtm.DishMenuTypeId
                               where lmtm.ActiveStatus == true
                               select new
                               {
                                   DishMenuTypeEntity = dm.DishMenuTypeEntity,
                                   LocationMenuTypeMapping = lmtm
                               };
            }

            var query = initialQuery.AsQueryable();
            if (locationId != null)
            {
                query = query.Where(x => x.LocationMenuTypeMapping.LocationSk == locationId);
            }

            if (sublocationId != null)
            {
                query = query.Where(x => x.LocationMenuTypeMapping.SubLocationSk == sublocationId);
            }

            return await query.Select(x => new MenuTypeDetailsModel
            {
                DishMenuTypeId = x.DishMenuTypeEntity.DishMenuTypeId,
                DishMenuTypeCode = x.DishMenuTypeEntity.DishMenuTypeCode,
                DishMenuTypeDesc = x.DishMenuTypeEntity.DishMenuTypeDesc,
                DisplayOrder = x.DishMenuTypeEntity.DisplayOrder,
                Location = x.LocationMenuTypeMapping.Location
            }).ToListAsync();
        }
    }
}
