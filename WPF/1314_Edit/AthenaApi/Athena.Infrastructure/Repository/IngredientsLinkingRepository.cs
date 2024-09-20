using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
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
    public class IngredientsLinkingRepository : IIngredientsLinkingRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientsLinkingRepository> _logger;

        public IngredientsLinkingRepository(ILogger<IngredientsLinkingRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<long>> GetDestinationIngredientLinkingStatus(List<long> ingSks)
        {
            return await _athenaDbcontext.IngredientsLinkingEntity.AsNoTracking().Where(i => ingSks.Contains(i.DestIngSk)).Select(x => x.DestIngSk).Distinct().ToListAsync();
        }

        public async Task<List<IngredientsLinkingEntity>> GetIngredientLinking(long ingSk)
        {
            return await _athenaDbcontext.IngredientsLinkingEntity.Where(i => i.SourceIngSk == ingSk).ToListAsync();
        }

        public async Task<bool> SaveIngredientLinking(SaveIngredientLinkingReqModel reqData)
        {
            List<IngredientsLinkingEntity> existingLinking = await _athenaDbcontext.IngredientsLinkingEntity.Where(i => i.SourceIngSk == reqData.SourceIngSk).ToListAsync();
            if (existingLinking.Any())
            {
                List<long> requestIngs = reqData.DestingationIngSk;

                List<IngredientsLinkingEntity> linksToDelete = existingLinking.Where(i => !requestIngs.Contains(i.DestIngSk)).ToList();

                if (linksToDelete.Any())
                    _athenaDbcontext.IngredientsLinkingEntity.RemoveRange(linksToDelete);

                List<long> existingLinkIngIds = existingLinking.Select(i => i.DestIngSk).ToList();
                if (existingLinkIngIds != null && existingLinkIngIds.Count > 0)
                    reqData.DestingationIngSk = reqData.DestingationIngSk.Where(i => !existingLinkIngIds.Contains(i)).ToList();
            }

            if (reqData.DestingationIngSk.Any())
            {
                foreach (long destIngSk in reqData.DestingationIngSk)
                {
                    IngredientsLinkingEntity newIngredientsLinkingEntity = new IngredientsLinkingEntity()
                    {
                        SourceIngSk = reqData.SourceIngSk,
                        DestIngSk = destIngSk,
                        CreatedBy = reqData.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ActiveStatus = reqData.ActiveStatus,
                    };
                    await _athenaDbcontext.IngredientsLinkingEntity.AddAsync(newIngredientsLinkingEntity);
                }
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }
    }
}
