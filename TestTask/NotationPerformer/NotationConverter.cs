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
    public class NotationConverter
    {
        private List<string> tokens;
        private Stack<string> operators;
        private Stack<string> result;
        private int index;

        private string Token
        {
            get { return tokens[index]; }
        }

        public Stack<string> PolishNotation
        {
            get { return result; }
        }

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
            string splitPattern = @"(?<=[ ,()+/*-])|(?=[ ,()+/*-])";

            return new List<string>(Regex.Split(tempExpr, splitPattern));
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
                else if (Helper.IsCloseBracket(Token))
                {
                    if (!PerformCloseBracket())
                    {
                        throw new InvalidBracketEcxeption("Lack of open brackets");
                    }
                }
                else if (Helper.IsDelimeter(Token))
                {
                    if (Helper.IsCorrectDelimeter(GetPrevNotSpace(), GetNextNotSpace()))
                    {
                        PerformDelimeter();
                    }
                    else
                    {
                        throw new DelimeterPositioningException("Invalid comma position");
                    }
                }
                else if (Helper.IsSpace(Token))
                {
                    index++;
                    continue;
                }
                else
                {
                    throw new InvalidTokenException("Invalid token <" + Token + "> in expression");
                }
                index++;
            }
            if (!IsExpressionParsed())
            {
                throw new InvalidBracketEcxeption("Lack of close brackets");
            }
            ReverseResult();
        }

        private bool IsUnary()
        {
            var maybeUnary = Token == "-" || Token == "+";
            var isOpenBracketPrev = Helper.IsOpenBracket(GetPrevNotSpace());
            var isOpenBracketNext = Helper.IsOpenBracket(GetNextNotSpace());
            var isNumberNext = Helper.IsNumber(GetNextNotSpace());
            var isFunctionNext = Helper.IsFunction(GetNextNotSpace());
            var isDelimeterPrev = Helper.IsDelimeter(GetPrevNotSpace());

            if (maybeUnary && (isOpenBracketPrev || isDelimeterPrev) && (isNumberNext || isFunctionNext || isOpenBracketNext))
            {
                return true;
            }
            return false;
        }

        private void PerformOperator()
        {
            var tokenPriority = Helper.Priorities[Token];
            var lastStackOperatorPriority = operators.Count() == 0 ? -1 : Helper.Priorities[operators.Peek()];

            if (IsUnary())
            {
                if (Token == "-")
                {
                    operators.Push("_");
                    return;
                }
                else
                {
                    operators.Push("#");
                    return;
                }
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

            while (fnPriority <= Helper.Priorities[operators.Peek()])
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

        private void PerformDelimeter()
        {
            while (operators.Count != 0 && operators.Peek() != "(")
            {
                result.Push(operators.Pop());
            }
        }

        private string GetPrevNotSpace()
        {
            for (int i = index - 1; i >= 0; i--)
            {
                if (!Helper.IsSpace(tokens[i]))
                {
                    return tokens[i];
                }
            }
            return "";
        }

        private string GetNextNotSpace()
        {
            for (int i = index + 1; i < tokens.Count; i++)
            {
                if (!Helper.IsSpace(tokens[i]))
                {
                    return tokens[i];
                }
            }
            return "";
        }

        private void ReverseResult()
        {
            Stack<string> temp = new Stack<string>();

            while (result.Count != 0)
            {
                if (result.Peek() != ",")
                {
                    temp.Push(result.Pop());
                }
                else
                {
                    result.Pop();
                }
            }

            result = temp;
        }

        private bool IsExpressionParsed()
        {
            return operators.Count == 0;
        }
    }
}
