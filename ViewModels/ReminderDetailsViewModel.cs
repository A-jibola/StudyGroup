using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class ReminderDetailsViewModel
    {
        public string ErrorMessage { get; set; }

        public Reminder Reminder { get; set; }

        public Group Group { get; set; }

        public string CurrentUser { get; set; }
    }
}
