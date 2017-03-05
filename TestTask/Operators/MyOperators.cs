using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Operators
{
    public interface IOperation
    {
        string Name { get; }
    }

    public interface IUnaryOperation : IOperation
    {
        float Calculate(float right);
    }

    public interface IBinaryOperation : IOperation
    {
        float Calculate(float left, float right);
    }

    public class Addition : IBinaryOperation
    {
        public string Name
        {
            get { return "+"; }
        }

        public float Calculate(float left, float right)
        {
            return right + left;
        }
    }

    public class Difference : IBinaryOperation
    {
        public string Name
        {
            get { return "-"; }
        }

        public float Calculate(float left, float right)
        {
            return right - left;
        }
    }

    public class Muliply : IBinaryOperation
    {
        public string Name
        {
            get { return "*"; }
        }

        public float Calculate(float left, float right)
        {
            return right * left;
        }
    }

    public class Division : IBinaryOperation
    {
        public string Name
        {
            get { return "/"; }
        }

        public float Calculate(float left, float right)
        {
            if (left == 0)
            {
                throw new DivideByZeroException();
            }
            return right / left;
        }
    }

    public class Pow : IBinaryOperation
    {
        public string Name
        {
            get { return "pow"; }
        }

        public float Calculate(float left, float right)
        {
            return (float)Math.Pow(right, left);
        }
    }

    public class UnaryMinus : IUnaryOperation
    {
        public string Name
        {
            get { return "_"; }
        }

        public float Calculate(float right)
        {
            return -right;
        }
    }

    public class UnaryPlus: IUnaryOperation
    {
        public string Name
        {
            get { return "#"; }
        }

        public float Calculate(float right)
        {
            return right;
        }
    }

    public class Sin : IUnaryOperation
    {
        public string Name
        {
            get { return "sin"; }
        }

        public float Calculate(float right)
        {
            return (float)Math.Sin(right);
        }
    }

    public class Cos: IUnaryOperation
    {
        public string Name
        {
            get { return "cos"; }
        }

        public float Calculate(float right)
        {
            return (float)Math.Cos(right);
        }
    }
}
