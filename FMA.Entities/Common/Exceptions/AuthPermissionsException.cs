using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Entities.Common.Exceptions
{
    public class AuthPermissionsException : Exception
    {
        /// <summary>
        /// Must contain a message
        /// </summary>
        /// <param name="message"></param>
        public AuthPermissionsException(string message)
            : base(message)
        { }
    }
}
