using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class RemindersListViewModel
    {
        public string GroupAdminID { get; set; }

        public string CurrentUser { get; set; }

        public IEnumerable<Reminder> Reminders { get; set; }

        public Group Group { get; set; }
    }
}
