using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class DocumentViewModel
    {
        public int GroupId { get; set; }

        public Document Document { get; set; }

        public string ErrorMessage { get; set; }

        public Group Group { get; set; }

        public string CurrentUser { get; set; }

    }
}
