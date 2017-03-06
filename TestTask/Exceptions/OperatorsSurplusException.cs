using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class OperatorsSurplusException : InvalidExpressionException
    {
        public OperatorsSurplusException()
            : base("Operators excess in expression") { }

        public OperatorsSurplusException(string message)
            : base(message) { }
    }
}
