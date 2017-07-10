using System;
using System.Diagnostics;

namespace GCDSearch
{
    /// <summary>
    /// This class provides opportunities to find the greatest common divisor for several integers 
    /// </summary>
    public static class GCD
    {

        /// <summary>
        /// This method allows us to find the greatest common divisor by Euclid's algorithm for 2 integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="first">First integer</param>
        /// <param name="second">Second integer</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfEuclid(out long time, int first, int second)
        {
            var start = Stopwatch.StartNew();
            int result = EuclidMain(first, second);
            time = start.ElapsedMilliseconds;
            return result;
        }


        /// <summary>
        /// This method allows us to find the greatest common divisor by Euclid's algorithm for set of integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="nums">Set of integers</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfEuclid(out long time, params int[] nums)
        {
            var start = Stopwatch.StartNew();
            if (nums.Length < 2)
                throw new ArgumentException();

            int tempGCD = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                tempGCD = EuclidMain(tempGCD, nums[i]);
            }
            time = start.ElapsedMilliseconds; 
            return tempGCD;
        }


        /// <summary>
        /// This method allows us to find the greatest common divisor by Stein algorithm for 2 integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="first">First integer</param>
        /// <param name="second">Second integer</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfStein(out long time, int first, int second)
        {
            var start = Stopwatch.StartNew();
            int result = SteinMain(first, second);
            time = start.ElapsedMilliseconds;
            return result;
        }


        /// <summary>
        /// This method allows us to find the greatest common divisor by Stein algorithm for set of integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="nums">Set of integers</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfStein(out long time, params int[] nums)
        {
            var start = Stopwatch.StartNew();
            if (nums.Length < 2)
                throw new ArgumentException();

            int tempGCD = Math.Abs(nums[0]);
            for (int i = 1; i < nums.Length; i++)
            {
                tempGCD = SteinMain(tempGCD, Math.Abs(nums[i]));
            }
            time = start.ElapsedMilliseconds;
            return tempGCD;
        }


        /// <summary>
        /// Stein algorithm for 2 integers
        /// </summary>
        /// <param name="a">First integer</param>
        /// <param name="b">Second integer</param>
        /// <returns>Greatest common divisor for 2 integers</returns>
        private static int SteinMain(int a, int b)
        {
            if (a == b) return a;
            if (a == 0) return b;
            if (b == 0) return a;

            if ((a & 1) != 1)
            {
                if ((b & 1) == 1)
                    return SteinMain(a >> 1, b);
                return SteinMain(a >> 1, b >> 1) << 1;
            }

            if ((b & 1) != 1)
                return SteinMain(a, b >> 1);

            return a > b ? SteinMain((a - b) >> 1, b) : SteinMain((b - a) >> 1, a);
        }


        /// <summary>
        /// Classical Euclid's algorithm for 2 integers
        /// </summary>
        /// <param name="a">First integer</param>
        /// <param name="b">Second integer</param>
        /// <returns>Greatest common divisor for 2 integers</returns>
        private static int EuclidMain(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : EuclidMain(b, a % b);
        }
    }
}
