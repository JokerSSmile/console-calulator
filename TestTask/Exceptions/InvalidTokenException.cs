using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class InvalidTokenException : InvalidExpressionException
    {
        public InvalidTokenException() { }

        public InvalidTokenException(string message)
            : base(message) { }
    }
}
