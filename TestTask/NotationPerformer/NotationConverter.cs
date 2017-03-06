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
            string splitPattern = @"(?<=[\t ,()+/*-])|(?=[\t ,()+/*-])";

            return new List<string>(Regex.Split(tempExpr, splitPattern));
        }

        public void Convert()
        {
            while (index < tokens.Count)
            {
                if (TokenInitializer.IsNumber(Token))
                {
                    result.Push(Token);
                }
                else if (TokenInitializer.IsOpenBracket(Token))
                {
                    operators.Push(Token);
                }
                else if (TokenInitializer.IsOperator(Token))
                {
                    PerformOperator();
                }
                else if (TokenInitializer.IsFunction(Token))
                {
                    PerformFunction();
                }
                else if (TokenInitializer.IsCloseBracket(Token))
                {
                    if (!PerformCloseBracket())
                    {
                        throw new InvalidBracketEcxeption();
                    }
                }
                else if (TokenInitializer.IsDelimeter(Token))
                {
                    if (TokenInitializer.IsCorrectDelimeter(GetPrevNotSpace(), GetNextNotSpace()))
                    {
                        PerformDelimeter();
                    }
                    else
                    {
                        throw new DelimeterPositioningException();
                    }
                }
                else if (TokenInitializer.IsSpace(Token))
                {
                    index++;
                    continue;
                }
                else
                {
                    throw new InvalidTokenException(Token);
                }
                index++;
            }
            if (!IsExpressionParsed())
            {
                throw new InvalidBracketEcxeption();
            }
            ReverseResult();
        }

        private bool IsUnary()
        {
            var maybeUnary = Token == "-" || Token == "+";
            var isOpenBracketPrev = TokenInitializer.IsOpenBracket(GetPrevNotSpace());
            var isOpenBracketNext = TokenInitializer.IsOpenBracket(GetNextNotSpace());
            var isNumberNext = TokenInitializer.IsNumber(GetNextNotSpace());
            var isFunctionNext = TokenInitializer.IsFunction(GetNextNotSpace());
            var isDelimeterPrev = TokenInitializer.IsDelimeter(GetPrevNotSpace());

            if (maybeUnary && (isOpenBracketPrev || isDelimeterPrev) && (isNumberNext || isFunctionNext || isOpenBracketNext))
            {
                return true;
            }
            return false;
        }

        private void PerformOperator()
        {
            var tokenPriority = TokenInitializer.Priorities[Token];
            var lastStackOperatorPriority = operators.Count() == 0 ? -1 : TokenInitializer.Priorities[operators.Peek()];

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
                while (tokenPriority <= TokenInitializer.Priorities[operators.Peek()])
                {
                    result.Push(operators.Pop());
                }
                operators.Push(Token);
            }
        }

        private void PerformFunction()
        {
            var fnPriority = TokenInitializer.Priorities[Token];

            while (fnPriority <= TokenInitializer.Priorities[operators.Peek()])
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
                if (!TokenInitializer.IsSpace(tokens[i]))
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
                if (!TokenInitializer.IsSpace(tokens[i]))
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
