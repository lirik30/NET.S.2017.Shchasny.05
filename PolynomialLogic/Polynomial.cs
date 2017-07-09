using System;
using System.Linq;

namespace PolynomialLogic
{
    public class Polynomial
    {
        //TODO: add possibility to do something if size = 0 or if one of params in operator overload is null

        private double[] coeffs;
        public int Size => coeffs.Length;

        #region ctors
        public Polynomial(params double[] coeffs)
        {
            if (coeffs == null)
                throw new ArgumentNullException();

            this.coeffs = new double[coeffs.Length];
            for (int i = 0; i < coeffs.Length; i++)
                this[i] = coeffs[i];
        }

        public Polynomial(Polynomial copy) : this(copy.coeffs) { }
        public Polynomial(int size) : this(new double[size]) { }

        #endregion

        #region Operator overload

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            CheckArguments(a, b);

            var result = a.Size > b.Size ? new Polynomial(a.Size) : new Polynomial(b.Size);
            for (int i = 0; i < result.Size; i++)
            {
                if (i >= a.Size) result[i] = b[i];
                else if (i >= b.Size) result[i] = a[i];
                else result[i] = a[i] + b[i];
            }

            return result;
        }

        public static Polynomial operator -(Polynomial a)
        {
            CheckArguments(a);
            var result = new Polynomial(a);
            for (int i = 0; i < result.Size; i++)
                result[i] = -result[i];
            return result;
        }

        public static Polynomial operator -(Polynomial a, Polynomial b) => a + (-b);

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            CheckArguments(a, b);
            if(a.Size == 0 || b.Size == 0)
                throw new ArgumentOutOfRangeException();

            var result = new Polynomial((a.Size - 1) + (b.Size - 1) + 1);

            for (int i = 0; i < a.Size; i++)
                for (int j = 0; j < b.Size; j++)
                    result[i + j] += a[i] * b[j];

            return result;
        }

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            if (Equals(a, null) && Equals(b, null)) return true;
            if (!Equals(a, null) && !Equals(b, null)) return a.Equals(b);
            return false;
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            if (Equals(a, null) && Equals(b, null)) return false;
            if (!Equals(a, null) && !Equals(b, null)) return !a.Equals(b);
            return true;
        }

        //public static bool operator == (Polynomial a, Polynomial b) => !Equals(b, null) ? !Equals(a, null) && a.Equals(b) : Equals(a, null);

        //public static bool operator != (Polynomial a, Polynomial b) => !Equals(b, null) ? !Equals(a, null) && !a.Equals(b) : !Equals(a, null);

        #endregion

        #region Static methods

        public static Polynomial Add(Polynomial a, Polynomial b)      => a + b;

        public static Polynomial Subtract(Polynomial a, Polynomial b) => a - b;

        public static Polynomial Multiply(Polynomial a, Polynomial b) => a * b;

        public static Polynomial Opposite(Polynomial a)               => -a;

        #endregion

        #region Object methods

        public override bool Equals(object obj)
        {
            var temp = obj as Polynomial;
            if (temp?.Size != Size)
                return false;

            for (int i = 0; i < this.Size; i++)
            {
                if (this.coeffs[i] != temp.coeffs[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return coeffs.GetHashCode();
        }

        public override string ToString()
        {
            string result = $"{coeffs[0]}";
            for (int i = 1; i < coeffs.Length; i++)
            {
                if (Math.Abs(coeffs[i] - 0) < double.Epsilon) continue;
                result += coeffs[i] > 0 ? $"+{coeffs[i]}x^{i}" : $"{coeffs[i]}x^{i}";
            }
            return result;
        }
        #endregion

        public double Calculate(double num)
        {
            double result = coeffs[0];
            for (int i = 1; i < Size; i++)
            {
                result += coeffs[i] * num;
                num *= num;
            }
            return result;
        }

        public double this[int index]
        {
            get => index > Size - 1 ? throw new ArgumentOutOfRangeException() : coeffs[index];
            set
            {
                if (index > Size - 1) throw new ArgumentOutOfRangeException();
                coeffs[index] = value;
            }
        }

        private static void CheckArguments(params object[] args)
        {
            if (args.Any(arg => arg == null))
            {
                throw new ArgumentNullException();
            }
        }
    }
}