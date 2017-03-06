using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask.InfixToPostfix;
using TestTask.Exceptions;
using System.Collections.Generic;

namespace CalculatorTests
{
    [TestClass]
    public class NotationConverterTests
    {
        private Stack<string> PushToStack(string[] values)
        {
            Stack<string> result = new Stack<string>();

            for (int i = values.Length - 1; i >= 0; i--)
            {
                result.Push(values[i]);
            }

            return result;
        }

        [TestMethod]
        public void CorrectIgnoringSpaces()
        {
            NotationConverter converter = new NotationConverter("            4+         5");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "5", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectSumOf2Digits()
        {
            NotationConverter converter = new NotationConverter("4+5");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "5", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectSumOf2Numbers()
        {
            NotationConverter converter = new NotationConverter("41+55");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "41", "55", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectDifferenceOf2Numbers()
        {
            NotationConverter converter = new NotationConverter("41-5");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "41", "5", "-" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectProductOf2Numbers()
        {
            NotationConverter converter = new NotationConverter("4*31");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "31", "*" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectDivisionOf2Numbers()
        {
            NotationConverter converter = new NotationConverter("4/31");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "31", "/" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectUnaryMinus()
        {
            NotationConverter converter = new NotationConverter("-4");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "_" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectUnaryPlus()
        {
            NotationConverter converter = new NotationConverter("+4");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "#" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectSin()
        {
            NotationConverter converter = new NotationConverter("sin(4)");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "sin" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectCos()
        {
            NotationConverter converter = new NotationConverter("cos(4)");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "cos" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectPow()
        {
            NotationConverter converter = new NotationConverter("pow(2, 3)");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "2", "3", "pow" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectSumOfManyNumbers()
        {
            NotationConverter converter = new NotationConverter("4+5+6+1+2+3+7");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "5", "+", "6", "+", "1", "+", "2", "+", "3", "+", "7", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectWorkWithDifferentPriorities()
        {
            NotationConverter converter = new NotationConverter("-4+5*6+1/2*3+7");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "4", "_", "5", "6", "*", "+", "1", "2", "/", "3", "*", "+", "7", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectWorkIfMultiplyFunctions()
        {
            NotationConverter converter = new NotationConverter("sin(pow(5, -cos(3)))");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "5", "3", "cos", "_", "pow", "sin" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectWorkWithMultiplyFunctionsAndOperators()
        {
            NotationConverter converter = new NotationConverter("-3+4/cos(pow(cos(1), sin(5)))");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "3", "_", "4", "1", "cos", "5", "sin", "pow", "cos", "/", "+" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectWorkWithMultiplyFunctionsAndOperatorsInPow()
        {
            NotationConverter converter = new NotationConverter("pow(cos(1) + pow(2 + 3, 2), sin(5))");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "1", "cos", "2", "3", "+", "2", "pow", "+", "5", "sin", "pow" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CorrectCommaProcessing()
        {
            NotationConverter converter = new NotationConverter("pow((3    )   ,(3))");
            converter.Convert();
            var result = converter.PolishNotation;
            var expected = PushToStack(new string[] { "3", "3", "pow" });

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ThrowsExeptionIfIncorrectBracket()
        {
            NotationConverter converter = new NotationConverter("())");
            converter.Convert();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ThrowsExeptionIfIncorrectBracket2()
        {
            NotationConverter converter = new NotationConverter("()(");
            converter.Convert();
        }
    }
}
