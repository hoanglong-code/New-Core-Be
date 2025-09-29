using Domain.Constants;
using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Extend
{
    [Serializable]
    public class ValidateException : BaseException
    {
        public ValidateException(string message, Exception? innerException = null) : base(message, ErrorCodeConstant.VALIDATION, innerException) { }
        public ValidateException(string message, IDictionary<string, object> payloads, Exception? innerException = null)
            : base(message, ErrorCodeConstant.VALIDATION, payloads, innerException) { }
    }
}
