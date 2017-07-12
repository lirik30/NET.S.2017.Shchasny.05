using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Double.Extensions
{
    /// <summary>
    /// Allows us to see a double-bit representation
    /// </summary>
    public static class DoubleExtensions
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct HelperStruct
        {
            [FieldOffset(0)]
            public double doubleNumber;
            [FieldOffset(0)]
            public readonly long helperLong;
        }

        /// <summary>
        /// Method allows us to see a double-bit representation.
        /// </summary>
        /// <param name="number">Double number</param>
        /// <returns>String with bit representation of a double number</returns>
        public static string ToBinary(this double number)
        {
            const int BIT_NUMBER = 64;
            var binaryNumber = new StringBuilder();

            var helperStruct = new HelperStruct {doubleNumber = number};

            long longNumber = helperStruct.helperLong;

            for (int i = 0; i < BIT_NUMBER; i++, longNumber >>= 1)
                binaryNumber.Append((longNumber & 1) == 1 ? "1" : "0");

            return new string(binaryNumber.ToString().ToCharArray().Reverse().ToArray());
        }
    }
}
