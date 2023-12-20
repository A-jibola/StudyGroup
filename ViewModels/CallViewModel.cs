using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class CallViewModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Username { get; set; }

        public string GroupAdminId { get; set; }

        public string CurrentUser { get; set; }
    }
}
