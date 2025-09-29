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
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, Exception? innerException = null) : base(message, ErrorCodeConstant.NOT_FOUND, innerException) { }
    }
}
