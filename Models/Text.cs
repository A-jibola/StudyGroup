using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Models
{
    public class Text
    {
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string SenderName { get; set; }

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateSent { get; set; }
    }
}
