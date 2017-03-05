using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestTask
{
    public static class Helper
    {
        public static List<string> Operators
        {
            get
            {
                return new List<string> { "-", "+", "*", "/" };
            }
        }

        public static List<string> Functions
        {
            get
            {
                return new List<string> { "sin", "cos", "pow" };
            }
        }

        public static Dictionary<string, int> Priorities
        {
            get
            {
                return new Dictionary<string, int>
                {
                    { "_", 3 },
                    { "sin", 4 },
                    { "cos", 4 },
                    { "pow", 4 },
                    { "*", 2 },
                    { "/", 2 },
                    { "-", 1 },
                    { "+", 1 },
                    { "(", 0 }
                };
            }
        }

        public static bool IsNumber(string token)
        {
            return Regex.IsMatch(token, @"^\d+$");
        }

        public static bool IsOperator(string token)
        {
            return Operators.Contains(token);
        }

        public static bool IsFunction(string token)
        {
            return Functions.Contains(token);
        }

        public static bool IsDelimeter(string token)
        {
            return token == ",";
        }

        public static bool IsOpenBracket(string token)
        {
            return token == "(";
        }

        public static bool IsCloseBracket(string token)
        {
            return token == ")";
        }
    }
}
