using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models.Email
{
    public class Email
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int EmailId { get; set; }
        [Required]
        public DateTime DateSent { get; set; }
        [Required]
        [StringLength(150)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [ForeignKey("EmailUser")]
        public int EmailUserId { get; set; }
        [ForeignKey("EmailTemplate")]
        public int EmailTemplateId { get; set; }
        public virtual EmailUser EmailUser { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
