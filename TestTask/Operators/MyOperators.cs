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

    public interface IUnaryOperation
    {
        float Calculate(float right);
    }

    public interface IBinaryoperation
    {
        float Calculate(float left, float right);
    }

    public class Addition : IBinaryoperation
    {
        public string Name
        {
            get { return "+"; }
        }

        public float Calculate(float left, float right)
        {
            return left + right;
        }
    }

    public class Difference : IBinaryoperation
    {
        public string Name
        {
            get { return "-"; }
        }

        public float Calculate(float left, float right)
        {
            return left - right;
        }
    }

    public class Muliply : IBinaryoperation
    {
        public string Name
        {
            get { return "*"; }
        }

        public float Calculate(float left, float right)
        {
            return left * right;
        }
    }

    public class Division : IBinaryoperation
    {
        public string Name
        {
            get { return "/"; }
        }

        public float Calculate(float left, float right)
        {
            return left / right;
        }
    }

    public class Pow : IBinaryoperation
    {
        public string Name
        {
            get { return "pow"; }
        }

        public float Calculate(float left, float right)
        {
            return (float)Math.Pow(left, right);
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
