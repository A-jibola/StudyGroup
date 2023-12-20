using StudyGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.ViewModels
{
    public class DocumentDetailsViewModel
    {

        public string CurrentUser { get; set; }

        public IEnumerable<Document> Documents { get; set; }

        public string GroupAdmin { get; set; }

        public Group Group { get; set; }
    }
}
