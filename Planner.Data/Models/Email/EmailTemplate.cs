using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models.Email
{
    public class EmailTemplate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int EmailTemplateId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(150)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
