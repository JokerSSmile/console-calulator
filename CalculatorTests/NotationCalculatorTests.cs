using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask.InfixToPostfix;
using TestTask.Exceptions;
using System.Collections.Generic;
using TestTask;

namespace CalculatorTests
{
    [TestClass]
    public class NotationCalculatorTests
    {
        private NotationCalculator calc;

        public NotationCalculatorTests()
        {
            calc = new NotationCalculator();
        }

        public bool NearlyEqual(float f1, float f2)
        {
            return Math.Abs(f1 - f2) < 0.0001;
        }

        private float GetExpressionResult(string infix)
        {
            NotationConverter converter = new NotationConverter(infix);
            converter.Convert();
            calc.Calculate(converter.PolishNotation);

            return calc.Result;
        }

        [TestMethod]
        public void ReturningCorrectResultWithAddition()
        {
            var result = GetExpressionResult("4+6+5");
            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        public void ReturningCorrectResultWithManyOperators()
        {
            var result = GetExpressionResult("4+5/2*5+(-1)+(3/2+1) + 8");
            Assert.AreEqual(result, 26);
        }

        [TestMethod]
        public void CorrectPowCalculating()
        {
            var result = GetExpressionResult("pow( 2 ,  cos(sin(pow(3, 1))) )");
            Assert.IsTrue(NearlyEqual(result, 1.9862f));
        }

        [TestMethod]
        public void ReturningCorrectResultWithManyFunctions()
        {
            var result = GetExpressionResult("pow( 2 ,  cos(sin(pow(3, 1))) )");
            Assert.IsTrue(NearlyEqual(result, 1.9862f));
        }

        [TestMethod]
        public void ReturningCorrectResult1()
        {
            var result = GetExpressionResult("3+(-sin(4)-pow(2, cos(1)))");
            Assert.IsTrue(NearlyEqual(result, -0.9680f));
        }

        [TestMethod]
        public void ReturningCorrectResult2()
        {
            var result = GetExpressionResult("-  cos (pow(pow(2, 2), cos(+3))  * (-1))");
            Assert.IsTrue(NearlyEqual(result, 1.9862f));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfIncorrectDelimetersPositioning()
        {
            var result = GetExpressionResult(",");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfIncorrectDelimetersPositioning2()
        {
            var result2 = GetExpressionResult("2,3");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfIncorrectDelimetersPositioning3()
        {
            var result3 = GetExpressionResult("pow(, 3)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfIncorrectDelimetersPositioning4()
        {
            var result4 = GetExpressionResult("pow(3, ,3)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfUnaryAfteroperator()
        {
            var result = GetExpressionResult("3+-5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfNumersSeparatedBySpace()
        {
            var result = GetExpressionResult("3 + 4 5");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void ErrorIfIncorrectInput()
        {
            var result = GetExpressionResult("4sdf");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void IncorrectExporessionInBrackets()
        {
            var result = GetExpressionResult("( + )");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException), AllowDerivedTypes = true)]
        public void IncorrectExporessionInBrackets2()
        {
            var result = GetExpressionResult("( 4+) )");
        }

        [TestMethod]
        public void CorrectWorkWithUnaryOperator1()
        {
            var result = GetExpressionResult("(-pow(2, 1))");
            Assert.AreEqual(result, -2);
        }

        [TestMethod]
        public void CorrectWorkWithUnaryOperator2()
        {
            var result = GetExpressionResult("3-(-3)");
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void CorrectWorkWithUnaryOperator3()
        {
            var result = GetExpressionResult("3-(-3)");
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void CorrectWorkWithUnaryOperator4()
        {
            var result = GetExpressionResult("-3-(+3) * (-1)");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), AllowDerivedTypes = true)]
        public void ExceptionIfDivisionByZero()
        {
            var result = GetExpressionResult("3/0");
        }

        [TestMethod]
        public void CorrectWorkWithManyBrackets()
        {
            var result = GetExpressionResult("(+(-(+3)+1))");
            Assert.AreEqual(result, -2);
        }

        [TestMethod]
        public void CorrectWorkWithManyBrackets2()
        {
            var result = GetExpressionResult("-((+1)-2+(4+(-6+5)))");
            Assert.AreEqual(result, -2);
        }


    }
}
