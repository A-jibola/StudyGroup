using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class TextMessageViewModel
    {
        public IEnumerable<Text> Texts { get; set; }

        public string CurrentUser { get; set; }

        public string GroupName { get; set; }

        public string CurrentUserName { get; set; }

        public Group Group { get; set; }

    }
}
