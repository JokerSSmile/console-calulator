using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    public class CalculationException : InvalidExpressionException
    {
        public CalculationException() { }

        public CalculationException(string message)
            : base(message) { }
    }
}
