using AngularWithOtherAPI.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.ViewModels
{
    public class CustomActionResultViewModel
    {
        public Exception Exception { get; set; }
        public Country Country { get; set; }
    }
}
