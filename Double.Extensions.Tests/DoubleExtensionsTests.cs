﻿using NUnit.Framework;

namespace Double.Extensions.Tests
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [TestCase(-255.255,                ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255,                 ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0,            ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue,         ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue,         ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon,          ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN,              ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0,                     ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.2,                     ExpectedResult = "0011111111001001100110011001100110011001100110011001100110011010")]
        [TestCase(1.02E-256,               ExpectedResult = "0000101011001000100000010010100100110011011111001010111011101101")]
        public string ToBinary_PositiveTests(double num)
        {
            return num.ToBinary();
        }
    }
}
