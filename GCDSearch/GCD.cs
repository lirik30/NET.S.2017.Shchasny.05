using System;

namespace GCDSearch
{
    public static class GCD
    {
        /// <summary>
        /// This method allows us to find the greatest common divisor by Euclid's algorithm for set of numbers
        /// </summary>
        /// <param name="nums">Set of integer numbers</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfEuclid(params int[] nums)
        {
            if (nums.Length < 2)
                throw new ArgumentException();

            int tempGCD = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                tempGCD = EuclidMain(tempGCD, nums[i]);
            }
            return tempGCD;
        }

        /// <summary>
        /// Classical Euclid's algorithm for 2 numbers
        /// </summary>
        /// <param name="a">First num</param>
        /// <param name="b">Second num</param>
        /// <returns>Greatest common divisor for 2 numbers</returns>
        private static int EuclidMain(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : EuclidMain(b, a % b);
        }
    }
}
