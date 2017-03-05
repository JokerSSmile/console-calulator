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

            while ((expression = AskForInput()) != "")
            {
                try
                {
                    NotationConverter converter = new NotationConverter(expression);
                    converter.Convert();
                    calculator.Calculate(converter.PolishNotation);
                }
                catch (InvalidBracketEcxeption ex)
                {
                    Console.WriteLine("Invalid brackets");
                }
                catch (InvalidTokenException ex)
                {
                    Console.WriteLine("InvalidToken: " + ex.Message);
                }
                catch (InvalidExpressionException ex)
                {
                    Console.WriteLine("Invalid expression");
                }
            }
        }

        private string AskForInput()
        {
            Console.WriteLine("Enter expression:");
            return Console.ReadLine();
        }
    }
}
