using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class GroupFormViewModel
    {
        public Group Group { get; set; }

        public string ErrorMessage { get; set; }

        public string CurrentUser { get; set; }
    }
}
