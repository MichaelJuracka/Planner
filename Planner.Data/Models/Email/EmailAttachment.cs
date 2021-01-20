using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models.Email
{
    public class EmailAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int EmailAttachmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Data { get; set; }
        [ForeignKey("Email")]
        public int EmailId { get; set; }
        public virtual Email Email { get; set; }
        
    }
}
