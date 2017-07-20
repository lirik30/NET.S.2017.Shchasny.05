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
        /// Delegate provides us with way to use Adapter method with different algorithms
        /// </summary>
        /// <param name="lhs">First integer</param>
        /// <param name="rhs">Second integer</param>
        /// <returns></returns>
        private delegate int Algorithm(int lhs, int rhs);


        /// <summary>
        /// This method allows us to find GCD of 2 integers with any algorithm, described by delegate
        /// </summary>
        /// <param name="algo">Delegate that describe used algorithm</param>
        /// <param name="lhs">First integer</param>
        /// <param name="rhs">Second integer</param>
        /// <param name="time">Method execution time</param>
        /// <returns></returns>
        private static int Adapter(Algorithm algo, int lhs, int rhs, out long time)
        {
            var start = Stopwatch.StartNew();
            int result = algo(Math.Abs(lhs), Math.Abs(rhs));
            time = start.ElapsedMilliseconds;
            return result;
        }


        /// <summary>
        /// This method allows us to find GCD of set of integers with any algorithm, described by delegate
        /// </summary>
        /// <param name="algo">Delegate that describe used algorithm</param>
        /// <param name="time">Method execution time</param>
        /// <param name="nums">Set of integers</param>
        /// <returns></returns>
        private static int Adapter(Algorithm algo, out long time, params int[] nums)
        {
            if (nums.Length < 2)
                throw new ArgumentException();

            var start = Stopwatch.StartNew();

            int tempGCD = Math.Abs(nums[0]);
            for (int i = 1; i < nums.Length; i++)
                tempGCD = algo(tempGCD, Math.Abs(nums[i]));
            time = start.ElapsedMilliseconds;
            return tempGCD;
        }


        /// <summary>
        /// This method allows us to find the greatest common divisor by Euclid's algorithm for 2 integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="first">First integer</param>
        /// <param name="second">Second integer</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfEuclid(out long time, int first, int second) 
            => Adapter(EuclidMain, first, second, out time);


        /// <summary>
        /// This method allows us to find the greatest common divisor by Euclid's algorithm for set of integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="nums">Set of integers</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfEuclid(out long time, params int[] nums) => Adapter(EuclidMain, out time, nums);


        /// <summary>
        /// This method allows us to find the greatest common divisor by Stein algorithm for 2 integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="first">First integer</param>
        /// <param name="second">Second integer</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfStein(out long time, int first, int second) 
            => Adapter(SteinMain, first, second, out time);


        /// <summary>
        /// This method allows us to find the greatest common divisor by Stein algorithm for set of integers
        /// and return the method execution time by out-parameter
        /// </summary>
        /// <param name="time">Method execution time</param>
        /// <param name="nums">Set of integers</param>
        /// <returns>Greatest common divisor</returns>
        public static int AlgorithmOfStein(out long time, params int[] nums) => Adapter(SteinMain, out time, nums);


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
        private static int EuclidMain(int a, int b) => b == 0 ? a : EuclidMain(b, a % b);
    }
}
