using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.ViewModels
{
    public class StandardViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class StandardGridViewModel
    {
        public int StandardId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
