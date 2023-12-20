using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public byte[] File { get; set; }

        public string FileType { get; set; }

        [Required]
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public DateTime DateSent { get; set; }
    }
}
