using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
