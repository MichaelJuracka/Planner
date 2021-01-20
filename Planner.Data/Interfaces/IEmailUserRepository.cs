using Planner.Data.Models.Email;

namespace Planner.Data.Interfaces
{
    public interface IEmailUserRepository : IRepository<EmailUser>
    {
        EmailUser GetDefaultUser();
    }
}
