using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Exceptions
{
    public class NameException: Exception
    {
        public NameException()
        {

        }

        public NameException(string message) : base(message)
        {

        }

        public NameException(string message, Exception inner) : base(message, inner)
        {

        }

        public NameException(string message, string name) : this(message)
        {

        }

        public bool StringStartWithNumber(string name)
        {
            return (Regex.IsMatch(name, @"^\d"));
        }


    }
}
