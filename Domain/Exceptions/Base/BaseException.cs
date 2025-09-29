using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Base
{
    public class BaseException : Exception
    {
        public string Message { get; set; }
        public string? Code { get; set; }
        public IDictionary<string, object> Payloads { get; private set; }

        protected BaseException(string message, string? code, Exception innerException) : base(message, innerException)
        {
            Message = message;
            Code = code;
        }

        protected BaseException(string message, string? code, IDictionary<string, object> payloads, Exception innerException) : base(message, innerException)
        {
            Message = message;
            Code = code;
            Payloads = payloads;
        }
    }
}
