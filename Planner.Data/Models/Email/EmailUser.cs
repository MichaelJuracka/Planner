using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models.Email
{
    public class EmailUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int EmailUserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public string SmtpServer { get; set; }
        [Required]
        public int SmtpPort { get; set; }
        public bool Default { get; set; }
    }
}
