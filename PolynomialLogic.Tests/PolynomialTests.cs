using System;
using System.Collections.Generic;
using static PolynomialLogic.Polynomial;
using NUnit.Framework;

namespace PolynomialLogic.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        #region AddTest
        private static IEnumerable<TestCaseData> AddTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(0.0, 5.0, -4.0, 3.5)).
                    Returns("1+5x^1+1,5x^2-2,95x^3+1,25x^4");

                yield return new TestCaseData(
                        new Polynomial(0.0, 5.0, -4.0, 3.5),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns("1+5x^1+1,5x^2-2,95x^3+1,25x^4");
                yield return new TestCaseData(
                        new Polynomial(1.0, 2.0, 3.0),
                        new Polynomial(-1.0000000011, -2.0, -3.0)).
                    Returns("");
            }
        }

        [Test, TestCaseSource(nameof(AddTestData_Positive))]
        public string Add_Positive(Polynomial a, Polynomial b)
        {
            return Add(a, b).ToString();
        }

        [TestCase(new double[] {1.0, 0, 5.5, -6.45, 1.25})]
        public void Add_ThrowsArgumentNullException(double[] a)
        {
            Assert.Throws<ArgumentNullException>(() => Add(new Polynomial(a), null));
            Assert.Throws<ArgumentNullException>(() => Add(null, new Polynomial(a)));
            Assert.Throws<ArgumentNullException>(() => Add(null, null));
        }
        #endregion

        #region SubtractTests
        private static IEnumerable<TestCaseData> SubtractTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(0.0, 5.0, -4.0, 3.5)).
                    Returns("1-5x^1+9,5x^2-9,95x^3+1,25x^4");
            }
        }

        [Test, TestCaseSource(nameof(SubtractTestData_Positive))]
        public string Subtract_Positive(Polynomial a, Polynomial b)
        {
            return Subtract(a, b).ToString();
        }

        [TestCase(new double[] { 1.0, 0, 5.5, -6.45, 1.25 })]
        public void Subtract_ThrowsArgumentNullException(double[] a)
        {
            Assert.Throws<ArgumentNullException>(() => Subtract(new Polynomial(a), null));
            Assert.Throws<ArgumentNullException>(() => Subtract(null, new Polynomial(a)));
            Assert.Throws<ArgumentNullException>(() => Subtract(null, null));
        }
        #endregion

        #region MultiplyTests
        private static IEnumerable<TestCaseData> MultiplyTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(0.0, 5.0, -4.0, 3.5)).
                    Returns("+5x^1-4x^2+31x^3-54,25x^4+51,3x^5-27,575x^6+4,375x^7");

                yield return new TestCaseData(
                        new Polynomial(0.0, 5.0, -4.0, 3.5),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns("+5x^1-4x^2+31x^3-54,25x^4+51,3x^5-27,575x^6+4,375x^7");
            }
        }
        
        [Test, TestCaseSource(nameof(MultiplyTestData_Positive))]
        public string Multiply_Positive(Polynomial a, Polynomial b)
        {
            return Multiply(a, b).ToString();
        }

        [TestCase(new double[] { 1.0, 0, 5.5, -6.45, 1.25 })]
        public void Multiply_ThrowsArgumentNullException(double[] a)
        {
            Assert.Throws<ArgumentNullException>(() => Multiply(new Polynomial(a), null));
            Assert.Throws<ArgumentNullException>(() => Multiply(null, new Polynomial(a)));
            Assert.Throws<ArgumentNullException>(() => Multiply(null, null));
        }
        #endregion

        #region OppositeTests
        private static IEnumerable<TestCaseData> OppositeTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns("-1-5,5x^2+6,45x^3-1,25x^4");
            }
        }

        [Test, TestCaseSource(nameof(OppositeTestData_Positive))]
        public string Opposite_Positive(Polynomial a)
        {
            return (-a).ToString();
        }

        [TestCase(new double[] { 1.0, 0, 5.5, -6.45, 1.25 })]
        public void Opposite_ThrowsArgumentNullException(double[] a)
        {
            Assert.Throws<ArgumentNullException>(() => Opposite(null));
        }


        #endregion

        #region Equals/NotEqualsTests
        private static IEnumerable<TestCaseData> EqualsTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(true);

                yield return new TestCaseData(
                        new Polynomial(4.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(false);

                yield return new TestCaseData(
                        null,
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(false);

                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        null).
                    Returns(false);

                yield return new TestCaseData(
                        null,
                        null).
                    Returns(true);
            }
        }

        private static IEnumerable<TestCaseData> NotEqualsTestData_Positive
        {
            get
            {
                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(false);

                yield return new TestCaseData(
                        new Polynomial(4.0, 0, 5.5, -6.45, 1.25),
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(true);

                yield return new TestCaseData(
                        null,
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25)).
                    Returns(true);

                yield return new TestCaseData(
                        new Polynomial(1.0, 0, 5.5, -6.45, 1.25),
                        null).
                    Returns(true);

                yield return new TestCaseData(
                        null,
                        null).
                    Returns(false);
            }
        }

        [Test, TestCaseSource(nameof(EqualsTestData_Positive))]
        public bool Equals_Positive(Polynomial a, Polynomial b)
        {
            return a == b;
        }

        [Test, TestCaseSource(nameof(NotEqualsTestData_Positive))]
        public bool NotEquals_Positive(Polynomial a, Polynomial b)
        {
            return a != b;
        }
        #endregion
    }
}
