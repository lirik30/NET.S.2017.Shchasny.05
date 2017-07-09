using System;
using NUnit.Framework;
using static GCDSearch.GCD;

namespace GCDSearch.Tests
{
    [TestFixture]
    public class GCDTests
    {
        [TestCase(1, 10, ExpectedResult = 1)]
        [TestCase(10, 5, ExpectedResult = 5)]
        [TestCase(661, 113, ExpectedResult = 1)]
        [TestCase(111, 432, ExpectedResult = 3)]
        [TestCase(24, 24, ExpectedResult = 24)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(5, 0, ExpectedResult = 5)]
        [TestCase(0, 15, ExpectedResult = 15)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(78, 294, 570, 36, ExpectedResult = 6)]
        [TestCase(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, ExpectedResult = 10)]
        [TestCase(2, 3, 7, 11, 13, 17, 19, ExpectedResult = 1)]
        public int AlgorithmOfEuclid_PositiveTests(params int[] nums)
        {
            TimeSpan time;
            return AlgorithmOfEuclid(out time, nums);
        }

        [TestCase(1)]
        [TestCase()]
        public void AlgorithmOfEuclid_ThrowsArgumentException(params int[] nums)
        {
            TimeSpan time;
            Assert.Throws<ArgumentException>(() => AlgorithmOfEuclid(out time, nums));
        }


        [TestCase(1, 10, ExpectedResult = 1)]
        [TestCase(10, 5, ExpectedResult = 5)]
        [TestCase(661, 113, ExpectedResult = 1)]
        [TestCase(111, 432, ExpectedResult = 3)]
        [TestCase(24, 24, ExpectedResult = 24)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(5, 0, ExpectedResult = 5)]
        [TestCase(0, 15, ExpectedResult = 15)]
        [TestCase(-5, 10, ExpectedResult = 5)]
        [TestCase(78, 294, 570, 36, ExpectedResult = 6)]
        [TestCase(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, ExpectedResult = 10)]
        [TestCase(2, 3, 7, 11, 13, 17, 19, ExpectedResult = 1)]
        public int AlgorithmOfStein_PositiveTests(params int[] nums)
        {
            TimeSpan time;
            return AlgorithmOfStein(out time, nums);
        }

        [TestCase(1)]
        [TestCase()]
        public void AlgorithmOfStein_ThrowsArgumentException(params int[] nums)
        {
            TimeSpan time;
            Assert.Throws<ArgumentException>(() => AlgorithmOfStein(out time, nums));
        }

    }
}
