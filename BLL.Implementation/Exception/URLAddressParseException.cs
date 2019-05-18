using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementation.Exception
{
    class URLAddressParseException : ArgumentException
    {
        public URLAddressParseException()
        {
        }

        public URLAddressParseException(string message) : base(message)
        {
        }

        public URLAddressParseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public URLAddressParseException(string message, string paramName) : base(message, paramName)
        {
        }

        public URLAddressParseException(string message, string paramName, System.Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected URLAddressParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
