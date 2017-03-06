using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Exceptions;
using TestTask.InfixToPostfix;

namespace TestTask
{
    class Application
    {
        private NotationCalculator calculator;

        public Application()
        {
            calculator = new NotationCalculator();
        }

        public void Run()
        {
            string expression;

            ShowHelp();

            while ((expression = AskForInput()) != "")
            {
                try
                {
                    NotationConverter converter = new NotationConverter(expression);
                    converter.Convert();
                    calculator.Calculate(converter.PolishNotation);
                    ShowResult();
                }
                catch (InvalidExpressionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("This calculator is supporting +-*/ operators, unary + and -,\nnasted brackets, functions sin(x), cos(x), pow(x,y).");
            Console.WriteLine();
        }

        private string AskForInput()
        {
            Console.WriteLine("Enter expression:");
            return Console.ReadLine();
        }

        private void ShowResult()
        {
            Console.WriteLine(">" + calculator.Result);
        }
    }
}
