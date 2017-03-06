using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class NumericVariableSurplusException : InvalidExpressionException
    {
        public NumericVariableSurplusException() { }

        public NumericVariableSurplusException(string message)
            : base(message) { }
    }
}
