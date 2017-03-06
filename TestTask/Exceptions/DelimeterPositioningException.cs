using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class DelimeterPositioningException : InvalidExpressionException
    {
        public DelimeterPositioningException()
            : base("Invalid comma position") { }

        public DelimeterPositioningException(string message)
            : base(message) { }
    }
}
