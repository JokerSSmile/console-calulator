using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    public class InvalidExpressionException : ApplicationException
    {
        public InvalidExpressionException()
            : base("Invalid expression") { }

        public InvalidExpressionException(string message)
            : base(message) { }
    }
}
