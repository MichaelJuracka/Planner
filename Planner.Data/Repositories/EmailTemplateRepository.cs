using Planner.Data.Interfaces;
using Planner.Data.Models.Email;

namespace Planner.Data.Repositories
{
    public class EmailTemplateRepository : BaseRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
