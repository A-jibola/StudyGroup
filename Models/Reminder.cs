using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime TimeToBeSent { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public bool IsSent { get; set; }

        [Required]
        public string SenderId { get; set; }

        [NotMapped]
        [ForeignKey("SenderId")]
        public virtual AppUser User { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [NotMapped]
        [ForeignKey("ReceiverId")]
        public virtual AppUser Receiver { get; set; }

        public int? GroupId { get; set; }

        [ForeignKey("GroupId")]
        [NotMapped]
        public Group Group { get; set; }

        public string UniqueTag { get; set; }
    }
}
