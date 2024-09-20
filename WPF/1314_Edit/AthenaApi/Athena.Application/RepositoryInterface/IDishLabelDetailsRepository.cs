using Athena.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface IDishLabelDetailsRepository
    {
        public Task<bool> SaveDishLabelDetails(int dishSk, List<int> dishLableTypeIds);
        public Task<bool> UpdateDishLabelDetails(int dishSk, List<int> dishLableTypeIds);
    }
}
