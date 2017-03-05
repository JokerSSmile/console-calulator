using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Operators;

namespace TestTask
{
    public class NotationCalculator
    {
        private List<IUnaryOperation> unaryOperations;
        private List<IBinaryoperation> binaryOperations;

        Stack<float> stack;

        public NotationCalculator()
        {
            unaryOperations = new List<IUnaryOperation> { new Sin(), new Cos(), new UnaryMinus() };
            binaryOperations = new List<IBinaryoperation> { new Addition(), new Difference(), new Division(), new Muliply(), new Pow() };
            stack = new Stack<float>();
        }

        public void Calculate(Stack<string> expression)
        {
            stack.Clear();


            while (expression.Count != 0)
            {
                string token = expression.Pop();

                if (Helper.IsNumber(token))
                {
                    stack.Push(int.Parse(token));
                    continue;
                }
                else if (Helper.IsUnary(token))
                {
                    foreach (var unary in unaryOperations.Where(op => op.Name == token))
                    {
                        stack.Push(unary.Calculate(stack.Pop()));
                    }
                }
                else
                {
                    foreach (var binary in binaryOperations.Where(op => op.Name == token))
                    {
                        stack.Push(binary.Calculate(stack.Pop(), stack.Pop()));
                    }
                }
            }
            if (stack.Count != 1)
            {
                Console.WriteLine("Calc Err");
            }
            else
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
