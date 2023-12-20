using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Goal { get; set; }

        [Range(1,10)]
        [Required]
        public byte NumberOfUsers { get; set; }

        public byte[] GroupImage { get; set; }

        public string ImageExtension { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public bool isClosed { get; set; }

        [Required]
        public bool VideoCall { get; set; }

        [Required]
        public bool VoiceCall { get; set; }

        [Required]
        public string GroupAdmin { get; set; }

        [ForeignKey("GroupAdmin")]
        [NotMapped]
        public virtual AppUser User { get; set; }
    }
}
