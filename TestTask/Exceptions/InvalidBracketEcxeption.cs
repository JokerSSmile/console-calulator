using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    public class InvalidBracketEcxeption : InvalidExpressionException
    {
        public InvalidBracketEcxeption()
            : base("Lack of brackets") { }

        public InvalidBracketEcxeption(string message)
            : base(message) { }
    }
}
