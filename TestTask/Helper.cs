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
            get { return new List<string> { "-", "+", "*", "/" }; }
        }

        public static List<string> Functions
        {
            get { return new List<string> { "sin", "cos", "pow" }; }
        }

        public static Dictionary<string, int> Priorities
        {
            get
            {
                return new Dictionary<string, int>
                {
                    { "sin", 4 },
                    { "cos", 4 },
                    { "pow", 4 },
                    { "#", 3 },
                    { "_", 3 },
                    { "*", 2 },
                    { "/", 2 },
                    { "-", 1 },
                    { "+", 1 },
                    { ",", 0 },
                    { "(", 0 }
                };
            }
        }

        public static bool IsUnary(string token)
        {
            return (new string[] { "_", "#", "sin", "cos" }).Contains(token);
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

        public static bool IsSpace(string token)
        {
            return  token == " " || token == "\t";
        }

        public static bool IsOpenBracket(string token)
        {
            return token == "(";
        }

        public static bool IsCloseBracket(string token)
        {
            return token == ")";
        }

        public static bool IsCorrectDelimeter(string prevToken, string nextToken)
        {
            var isCorrectPrev = IsNumber(prevToken) || IsCloseBracket(prevToken) || IsSpace(prevToken);
            var isCorrectNext = IsNumber(nextToken) || IsOpenBracket(nextToken) ||
                nextToken == "+" || nextToken == "-" || IsSpace(nextToken) || IsFunction(nextToken);
            return isCorrectPrev && isCorrectNext;
        }
    }
}
