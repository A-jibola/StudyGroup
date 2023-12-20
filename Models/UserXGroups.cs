using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Models
{
    public class UserXGroups
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [NotMapped]
        public virtual AppUser User { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        [NotMapped]
        public virtual Group Group { get; set; }
    }
}
