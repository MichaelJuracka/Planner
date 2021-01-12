using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Data.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
