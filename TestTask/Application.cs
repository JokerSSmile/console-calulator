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
                    ShowResult();
                }
                catch (InvalidBracketEcxeption ex)
                {
                    Console.WriteLine("Invalid brackets!");
                }
                catch (InvalidTokenException ex)
                {
                    Console.WriteLine("InvalidToken: " + ex.Message + "!");
                }
                catch (CalculationException ex)
                {
                    Console.WriteLine("Invalid expression: calculation error!");
                }
                catch (InvalidExpressionException ex)
                {
                    Console.WriteLine("Invalid expression!");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Division by zero not allowed!");
                }
            }
        }

        private string AskForInput()
        {
            Console.WriteLine("Enter expression:");
            return Console.ReadLine();
        }

        private void ShowResult()
        {
            Console.WriteLine("=" + calculator.Result);
        }
    }
}
