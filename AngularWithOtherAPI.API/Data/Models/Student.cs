using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int StandardId { get; set; }
        public Standard Standard { get; set; }
    }
}
