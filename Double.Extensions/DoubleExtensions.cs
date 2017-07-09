using System;
using System.Linq;

namespace Double.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToBinary(this double number)
        {
            switch (number)
            {
                case double.NaN:
                    return "1111111111111000000000000000000000000000000000000000000000000000";
                case double.NegativeInfinity:
                    return "1111111111110000000000000000000000000000000000000000000000000000";
                case double.PositiveInfinity:
                    return "0111111111110000000000000000000000000000000000000000000000000000";
            }

            string binaryView = number >= 0 ? "0" : "1";
            number = Math.Abs(number);
            const int MANTISSA_LENGTH = 53;
            double remainder = number % 1;

            string binaryIntPart = IntPartIntoBinary(number - number % 1);
            if (binaryIntPart.Length > MANTISSA_LENGTH)
                binaryIntPart = binaryIntPart.Substring(0, MANTISSA_LENGTH);

            int binaryRemLength = MANTISSA_LENGTH - binaryIntPart.Length;
            int shift;
            string binaryRemPart = RemPartIntoBinary(remainder, out shift);
            if(binaryRemPart.Length > binaryRemLength)
                binaryRemPart = binaryRemPart.Substring(binaryRemPart.Length - binaryRemLength + 1);

            int exp = number >= 1 ? 1023 + binaryIntPart.Length - 1 : 1023 + shift - 1;
            if (exp < 0 || (shift == 0 && String.IsNullOrEmpty(binaryIntPart))) exp = 0;
            if (binaryIntPart.Length == MANTISSA_LENGTH) exp = 2046;
            string exponent = Convert.ToString(exp, 2);
            exponent = exponent.PadLeft(11, '0');

            binaryView += exponent;
            if(binaryIntPart.Length > 1)
                binaryView += binaryIntPart.Substring(1);
            binaryView += binaryRemPart;
            binaryView = binaryView.PadRight(64, '0');

            return binaryView;
        }

        private static string IntPartIntoBinary(double num)
        {
            string intPart = "";
            while (num >= 1)
            {
                intPart += ((int)(num % 2)).ToString();
                num /= 2;
            }
            return new string(intPart.ToCharArray().Reverse().ToArray());
        }

        private static string RemPartIntoBinary(double rem, out int shift)//, int length)
        {
            string remPart = "";
            shift = 0;
            while (rem % 1 - 0 >= double.Epsilon)// && length-- != 0)
            {
                remPart += ((int)(rem *= 2)).ToString();
                shift--;
                if (rem > 1) rem -= 1;
            }
            return remPart;
        }
    }
}
