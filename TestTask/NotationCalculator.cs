using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class NotationCalculator
    {
        Stack<float> stack;

        public NotationCalculator()
        {
            stack = new Stack<float>();
        }

        public void Calculate(Stack<string> expression)
        {
            stack.Clear();

            //try
            //{
                while (expression.Count != 0)
                {
                    string token = expression.Pop();

                    if (Helper.IsNumber(token))
                    {
                        stack.Push(int.Parse(token));
                        continue;
                    }
                    switch (token)
                    {
                        case "+":
                            PerformAddition(stack.Pop(), stack.Pop());
                            break;
                        case "-":
                            PerformDifference(stack.Pop(), stack.Pop());
                            break;
                        case "*":
                            PerformMultiplication(stack.Pop(), stack.Pop());
                            break;
                        case "/":
                            PerformDivision(stack.Pop(), stack.Pop());
                            break;
                        case "pow":
                            PerformPow(stack.Pop(), stack.Pop());
                            break;
                        case "_":
                            PerformUnaryMinus(stack.Pop());
                            break;
                        case "sin":
                            PerformSin(stack.Pop());
                            break;
                        case "cos":
                            PerformCos(stack.Pop());
                            break;
                    }
                    /*
                    if (token == "+")
                    {
                        var right = stack.Pop();
                        var left = stack.Pop();

                        stack.Push(left + right);
                    }
                    else if (token == "_")
                    {
                        stack.Push(-stack.Pop());
                    }
                    else if (token == "-")
                    {
                        var left = stack.Pop();
                        var right = stack.Pop();

                        stack.Push(right - left);
                    }
                    else if (token == "*")
                    {
                        var left = stack.Pop();
                        var right = stack.Pop();

                        stack.Push(left * right);
                    }
                    else if (token == "/")
                    {
                        var left = stack.Pop();
                        var right = stack.Pop();

                        stack.Push(right / left);
                    }
                    else if (token == "sin")
                    {
                        var val = stack.Pop();

                        stack.Push((float)Math.Sin(val));
                    }
                    else if (token == "cos")
                    {
                        var val = stack.Pop();

                        stack.Push((float)Math.Cos(val));
                    }
                    else if (token == "pow")
                    {
                        var left = stack.Pop();
                        var right = stack.Pop();

                        stack.Push((float)Math.Pow(right, left));
                    }
                    */
                }
                

                if (stack.Count != 1)
                {
                    Console.WriteLine("Calc Err");
                }
                else
                {
                    Console.WriteLine(stack.Pop());
                    //Console.WriteLine(stack.Pop());
                }
            //}
            //catch (InvalidOperationException)
            //{

            //}
        }

        private void PerformAddition(float right, float left)
        {
            stack.Push(left + right);
        }

        private void PerformDifference(float right, float left)
        {
            stack.Push(left - right);
        }

        private void PerformMultiplication(float right, float left)
        {
            stack.Push(left * right);
        }

        private void PerformDivision(float right, float left)
        {
            stack.Push(left / right);
        }

        private void PerformPow(float right, float left)
        {
            stack.Push((float)Math.Pow(left, right));
        }

        private void  PerformUnaryMinus(float right)
        {
            stack.Push(-right);
        }

        private void PerformSin(float right)
        {
            stack.Push((float)Math.Sin(right));
        }

        private void PerformCos(float right)
        {
            stack.Push((float)Math.Cos(right));
        }
    }
}
