using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class ViewAGroupViewModel
    {
        public Group Group { get; set; }

        public IEnumerable<AppUser> GroupMembers { get; set; }

        public IEnumerable<AppUser> BlockedUsers { get; set; }

        public string CurrentUser { get; set; }

    }
}
