using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using TestTask.Exceptions;

namespace TestTask.InfixToPostfix
{
    //     (8+2*5)/(1+3*2—4)          6
    //     sin(cos(3)/4*9)            -0.7920193

    public class NotationConverter
    {
        private List<string> tokens;
        private Stack<string> operators;
        private Stack<string> result;
        private int index;

        public NotationConverter(string expression)
        {
            operators = new Stack<string>();
            result = new Stack<string>();
            SetString(expression);
        }

        public void SetString(string str)
        {
            tokens = new List<string>(Tokenize(str));
            tokens.Insert(0, "(");
            tokens.Add(")");
            tokens.RemoveAll(token => token == "");
        }

        private List<string> Tokenize(string expression)
        {
            string tempExpr = expression;
            string delSpacePattern = @"\s+";
            string splitPattern = @"(?<=[,()+/*-])|(?=[,()+/*-])";

            expression = Regex.Replace(expression, delSpacePattern, "");
            return new List<string>(Regex.Split(expression, splitPattern));
        }

        public void Convert()
        {
            while (index < tokens.Count)
            {
                if (Helper.IsNumber(Token))
                {
                    result.Push(Token);
                }
                else if (Helper.IsOpenBracket(Token))
                {
                    operators.Push(Token);
                }
                else if (Helper.IsOperator(Token))
                {
                    PerformOperator();
                }
                else if (Helper.IsFunction(Token))
                {
                    PerformFunction();
                }
                else if (Helper.IsDelimeter(Token))
                {
                    index++;
                    continue;
                }
                else if (Helper.IsCloseBracket(Token))
                {
                    if (!PerformCloseBracket())
                    {
                        //Console.WriteLine("Invalid brackets");
                        //return;
                        throw new InvalidBracketEcxeption();
                    }
                }
                else
                {
                    //Console.WriteLine("Invalid token <" + Token + ">");
                    //return;
                    throw new InvalidTokenException(Token);
                }
                index++;
            }
            if (!IsExpressionParsed())
            {
                //Console.WriteLine("Invalid expression");
                //return;
                throw new InvalidExpressionException();
            }
            ReverseResult();

            foreach (var el in result)
            {
                Console.Write(el + " ");
            }
            Console.WriteLine();
        }

        private bool IsUnaryMinus()
        {
            var isMinus = Token == "-";
            var isOpenBracketPrev = Helper.IsOpenBracket(tokens[index - 1]);
            var isOpenBracketNext = Helper.IsOpenBracket(tokens[index + 1]);
            var isNumberNext = Helper.IsNumber(tokens[index + 1]);
            var isFunctionNext = Helper.IsFunction(tokens[index + 1]);

            if (isMinus && isOpenBracketPrev && (isNumberNext || isFunctionNext || isOpenBracketNext))
            {
                return true;
            }
            return false;
        }

        private void PerformOperator()
        {
            var tokenPriority = Helper.Priorities[Token];
            var lastStackOperatorPriority = operators.Count() == 0 ? -1 : Helper.Priorities[operators.Peek()];

            if (IsUnaryMinus())
            {
                operators.Push("_");
                return;
            }
            if (tokenPriority > lastStackOperatorPriority)
            {
                operators.Push(Token);
            }
            else
            {
                while (tokenPriority <= Helper.Priorities[operators.Peek()])
                {
                    result.Push(operators.Pop());
                }
                operators.Push(Token);
            }
        }

        private void PerformFunction()
        {
            var fnPriority = Helper.Priorities[Token];

            while (fnPriority <= Helper.Priorities[operators.Peek()])//TODO: maybe >= was <=
            {
                result.Push(operators.Pop());
            }
            operators.Push(Token);
        }

        private bool PerformCloseBracket()
        {
            while (operators.Count != 0 && operators.Peek() != "(")
            {
                result.Push(operators.Pop());
            }
            if (operators.Count > 0)
            {
                operators.Pop();
                return true;
            }
            return false;
        }

        private void ReverseResult()
        {
            Stack<string> temp = new Stack<string>();

            while (result.Count != 0)
                temp.Push(result.Pop());

            result = temp;
        }

        private bool IsExpressionParsed()
        {
            return operators.Count == 0;
        }

        private string Token
        {
            get { return tokens[index]; }
        }

        public Stack<string> PolishNotation
        {
            get { return result; }
        }
    }
}
