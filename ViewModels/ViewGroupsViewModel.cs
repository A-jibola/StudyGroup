using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class ViewGroupsViewModel
    {
        public IEnumerable<Group> Groups { get; set; }

        public string ErrorMessage { get; set; }
    }
}
