using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    class InvalidBracketEcxeption : InvalidExpressionException
    {
        public InvalidBracketEcxeption() { }

        public InvalidBracketEcxeption(string message)
            : base(message) { }
    }
}
