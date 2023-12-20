using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class UserIndexViewModel
    {
        public string Username { get; set; }

        public string UserBio { get; set; }

        public IEnumerable<Reminder> Reminders { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public string PartialName { get; set; }
    }
}
