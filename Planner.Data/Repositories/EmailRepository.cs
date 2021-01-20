using Planner.Data.Interfaces;
using Planner.Data.Models.Email;

namespace Planner.Data.Repositories
{
    public class EmailRepository : BaseRepository<Email>, IEmailRepository
    {
        public EmailRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
