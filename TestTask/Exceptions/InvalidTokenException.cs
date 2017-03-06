using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Exceptions
{
    public class InvalidTokenException : InvalidExpressionException
    {
        public InvalidTokenException()
            : base("Invalid token in expression") { }

        public InvalidTokenException(string token)
            : base("Invalid token <" + token + "> in expression") { }
    }
}
