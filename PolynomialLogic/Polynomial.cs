using System;
using System.Linq;
using System.Configuration;

namespace PolynomialLogic
{
    //TODO: ConfigurationManager not found

    /// <summary>
    /// Class provides methods for working with polynomials of a real variable.
    /// </summary>
    public sealed class Polynomial
    {
        private double[] coeffs;
        private static readonly double epsilon;

        /// <summary>
        /// Degree of a polynomial
        /// </summary>
        public int Degree => coeffs.Length;

        #region ctors

        static Polynomial()
        {
            if (!Double.TryParse(ConfigurationManager.AppSettings["epsilon"], out epsilon))
                epsilon = 1E-6;
        }

        /// <summary>
        /// Create new polynomial with coefficients like the elements of array
        /// </summary>
        /// <param name="coeffs">Array of coefficients</param>
        public Polynomial(params double[] coeffs)
        {
            if (coeffs == null)
                throw new ArgumentNullException();

            this.coeffs = new double[coeffs.Length];
            for (int i = 0; i < coeffs.Length; i++)
                this[i] = coeffs[i];
        }

        /// <summary>
        /// Create copy of polynomial
        /// </summary>
        /// <param name="copy">Polynomial to copy</param>
        public Polynomial(Polynomial copy) : this(copy.coeffs) { }

        /// <summary>
        /// Create polynomial with zero coefficients. Need to specify the degree
        /// </summary>
        /// <param name="degree">Degree of polynomial</param>
        public Polynomial(int degree) : this(new double[degree]) { }

        #endregion

        #region Operator overload
        
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            CheckArguments(lhs, rhs);
            
            var result = lhs.Degree > rhs.Degree ? new Polynomial(lhs.Degree) : new Polynomial(rhs.Degree);
            for (int i = 0; i < result.Degree; i++)
            {
                if (i >= lhs.Degree) result[i] = rhs[i];
                else if (i >= rhs.Degree) result[i] = lhs[i];
                else result[i] = lhs[i] + rhs[i];
            }

            return result;
        }

        public static Polynomial operator -(Polynomial lhs)
        {
            CheckArguments(lhs);
            var result = new Polynomial(lhs);
            for (int i = 0; i < result.Degree; i++)
                result[i] = -result[i];
            return result;
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs) => lhs + (-rhs);

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            CheckArguments(lhs, rhs);
            if(lhs.Degree == 0 || rhs.Degree == 0)
                throw new ArgumentOutOfRangeException();

            var result = new Polynomial((lhs.Degree - 1) + (rhs.Degree - 1) + 1);

            for (int i = 0; i < lhs.Degree; i++)
                for (int j = 0; j < rhs.Degree; j++)
                    result[i + j] += lhs[i] * rhs[j];

            return result;
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (Equals(lhs, null) && Equals(rhs, null)) return true;
            if (!Equals(lhs, null) && !Equals(rhs, null)) return lhs.Equals(rhs);
            return false;
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            if (Equals(lhs, null) && Equals(rhs, null)) return false;
            if (!Equals(lhs, null) && !Equals(rhs, null)) return !lhs.Equals(rhs);
            return true;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Summarizes the polynomials
        /// </summary>
        /// <param name="lhs">First added</param>
        /// <param name="rhs">Second added</param>
        /// <returns>REsult of adding of polynomials if it's possible</returns>
        public static Polynomial Add(Polynomial lhs, Polynomial rhs)      => lhs + rhs;

        /// <summary>
        /// Subtract one polynomial from another
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>Difference</returns>
        public static Polynomial Subtract(Polynomial lhs, Polynomial rhs) => lhs - rhs;

        /// <summary>
        /// Multiplies the polynomials
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>Product of the polynomials</returns>
        public static Polynomial Multiply(Polynomial lhs, Polynomial rhs) => lhs * rhs;

        /// <summary>
        /// Returns the polynomial multiplied by -1
        /// </summary>
        /// <param name="lhs"></param>
        /// <returns>Opposite polynomial</returns>
        public static Polynomial Opposite(Polynomial lhs)               => -lhs;

        #endregion

        #region Object methods

        /// <summary>
        /// Comapare instance with the another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if it has same coefficients, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var temp = obj as Polynomial;
            if (temp?.Degree != Degree)
                return false;

            for (int i = 0; i < this.Degree; i++)
            {
                if (Math.Abs(this[i] - temp[i]) > Double.Epsilon)
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return coeffs.GetHashCode();
        }

        /// <summary>
        /// String representation of the polynomial
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = Math.Abs(this[0]) > epsilon ? $"{this[0]}" : "";
            for (int i = 1; i < Degree; i++)
            {
                if (Math.Abs(this[i]) <= epsilon) continue;
                result += this[i] > 0 ? $"+{this[i]}x^{i}" : $"{this[i]}x^{i}";
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Allows us to get value of polynomial of the specific variable
        /// </summary>
        /// <param name="num">Set value of the variable</param>
        /// <returns>Value of the polynomial</returns>
        public double Calculate(double num)
        {
            double result = coeffs[0];
            for (int i = 1; i < Degree; i++)
            {
                result += coeffs[i] * num;
                num *= num;
            }
            return result;
        }

        public double this[int index]
        {
            get => index > Degree - 1 ? throw new ArgumentOutOfRangeException() : coeffs[index];
            set
            {
                if (index > Degree - 1) throw new ArgumentOutOfRangeException();
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