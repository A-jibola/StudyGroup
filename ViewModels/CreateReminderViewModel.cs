using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class CreateReminderViewModel
    {
        public int GroupId { get; set; }

        public Reminder Reminder { get; set; }

        public string UniqueTag { get; set; }

        public Group Group { get; set; }

        public string CurrentUser { get; set; }
    }
}
