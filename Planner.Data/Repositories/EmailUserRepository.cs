using Planner.Data.Interfaces;
using Planner.Data.Models.Email;
using System.Linq;

namespace Planner.Data.Repositories
{
    public class EmailUserRepository : BaseRepository<EmailUser>, IEmailUserRepository
    {
        public EmailUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
        public EmailUser GetDefaultUser()
        {
            return dbSet.FirstOrDefault(x => x.Default);
        }
    }
}
