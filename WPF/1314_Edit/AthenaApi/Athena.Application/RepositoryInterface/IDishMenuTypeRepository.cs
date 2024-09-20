using Athena.Domain.Entities;
using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishMenuTypeRepository
    {
        public Task<List<MenuTypeDetailsModel>> GetDishMenuTypes(int? locationId = null, int? sublocationId = null);
    }
}
