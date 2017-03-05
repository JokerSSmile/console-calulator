using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class InvalidExpressionException : ApplicationException
    {
        public InvalidExpressionException() { }

        public InvalidExpressionException(string message)
            : base(message) { }
    }
}
