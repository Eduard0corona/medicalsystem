using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Application.Authorization.Common.Exceptions
{
    public class UserAlreadyRegisteredException : Exception
    {
        public UserAlreadyRegisteredException(string message) : base(message)
        {
        }

        public UserAlreadyRegisteredException(string[] errors) : base("User already exist.")
        {
            Errors = errors;
        }

        public string[]? Errors { get; set; }
    }
}
