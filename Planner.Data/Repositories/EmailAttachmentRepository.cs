using Planner.Data.Interfaces;
using Planner.Data.Models.Email;

namespace Planner.Data.Repositories
{
    public class EmailAttachmentRepository : BaseRepository<EmailAttachment>, IEmailAttachmentRepository
    {
        public EmailAttachmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
