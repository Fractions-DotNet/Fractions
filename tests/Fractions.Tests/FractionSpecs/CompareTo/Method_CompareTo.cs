using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.CompareTo;

[TestFixture]
public class When_comparing_two_fractions : Spec {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            // two positive numbers
            yield return new TestCaseData(new Fraction(5), new Fraction(4)).Returns(1);
            yield return new TestCaseData(new Fraction(4), new Fraction(5)).Returns(-1);
            yield return new TestCaseData(new Fraction(5), new Fraction(5)).Returns(0);
            yield return new TestCaseData(new Fraction(1, 5), new Fraction(1, 10)).Returns(1);
            yield return new TestCaseData(new Fraction(1, 10), new Fraction(10, 100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(1, 10), new Fraction(-10, -100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-100, -1000, false), new Fraction(-10, -100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(0.2m), new Fraction(-2, -10, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-2, -10, false), new Fraction(0.2m)).Returns(0);
            yield return new TestCaseData(new Fraction(3, 3, false), new Fraction(2, 2, false)).Returns(0);
            yield return new TestCaseData(new Fraction(4, 3, false), new Fraction(3, 2, false)).Returns(-1);

            yield return new TestCaseData(new Fraction(0.1m), new Fraction(-2, -10, false)).Returns(-1);
            yield return new TestCaseData(new Fraction(0.3m), new Fraction(-2, -10, false)).Returns(1);
            yield return new TestCaseData(new Fraction(-2, -10, false), new Fraction(0.1m)).Returns(1);
            yield return new TestCaseData(new Fraction(-2, -10, false), new Fraction(0.3m)).Returns(-1);
            // two negative numbers
            yield return new TestCaseData(new Fraction(-5), new Fraction(-4)).Returns(-1);
            yield return new TestCaseData(new Fraction(-4), new Fraction(-5)).Returns(1);
            yield return new TestCaseData(new Fraction(-5), new Fraction(-5)).Returns(0);
            yield return new TestCaseData(new Fraction(-1, 5), new Fraction(-1, 10)).Returns(-1);
            yield return new TestCaseData(new Fraction(-1, 10), new Fraction(10, -100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-1, 10), new Fraction(-10, 100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-100, 1000, false), new Fraction(10, -100, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-3, 3, false), new Fraction(-2, 2, false)).Returns(0);
            yield return new TestCaseData(new Fraction(-4, 3, false), new Fraction(-3, 2, false)).Returns(1);
            // numbers with opposite signs
            yield return new TestCaseData(Fraction.One, Fraction.MinusOne).Returns(1);
            yield return new TestCaseData(Fraction.MinusOne, Fraction.One).Returns(-1);
            yield return new TestCaseData(new Fraction(1, 5), new Fraction(-1, 10)).Returns(1);
            yield return new TestCaseData(new Fraction(1, 10), new Fraction(-10, 100, false)).Returns(1);
            yield return new TestCaseData(new Fraction(-1, 10), new Fraction(1, 5)).Returns(-1);
            yield return new TestCaseData(new Fraction(-10, 100, false), new Fraction(1, 10)).Returns(-1);
            // zero with zero
            yield return new TestCaseData(Fraction.Zero, Fraction.Zero).Returns(0);
            yield return new TestCaseData(Fraction.Zero, new Fraction(BigInteger.Zero, 4, false)).Returns(0);
            yield return new TestCaseData(Fraction.Zero, new Fraction(BigInteger.Zero, -4, false)).Returns(0);
            // zero with positive numbers
            yield return new TestCaseData(Fraction.Zero, Fraction.One).Returns(-1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(5, 4)).Returns(-1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(4, 5)).Returns(-1);
            yield return new TestCaseData(Fraction.Zero, Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(5, 0, false)).Returns(-1);
            // zero with negative numbers
            yield return new TestCaseData(Fraction.Zero, Fraction.MinusOne).Returns(1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(-5, 4)).Returns(1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(-4, 5)).Returns(1);
            yield return new TestCaseData(Fraction.Zero, Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(Fraction.Zero, new Fraction(-5, 0, false)).Returns(1);
            // positive infinity with positive infinity
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.PositiveInfinity).Returns(0);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(5, 0, false)).Returns(0);
            // positive infinity with any other number
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(5, 4)).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.One).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(4, 5)).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.Zero).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.MinusOne).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-5, 4)).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-4, 5)).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-5, 0, false)).Returns(1);
            // negative infinity with negative infinity
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.NegativeInfinity).Returns(0);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-5, 0, false)).Returns(0);
            // negative infinity with any other number
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(5, 0, false)).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(5, 4)).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.One).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(4, 5)).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.Zero).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.MinusOne).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-5, 4)).Returns(-1);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-4, 5)).Returns(-1);
            // NaN with NaN
            yield return new TestCaseData(Fraction.NaN, Fraction.NaN).Returns(0);
            // NaN with any number
            yield return new TestCaseData(Fraction.NaN, Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, new Fraction(5, 4)).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, Fraction.One).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, new Fraction(4, 5)).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, Fraction.Zero).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, Fraction.MinusOne).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, new Fraction(-5, 4)).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, new Fraction(-4, 5)).Returns(-1);
            yield return new TestCaseData(Fraction.NaN, Fraction.NegativeInfinity).Returns(-1);
            // Any number with NaN
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.NaN).Returns(1);
            yield return new TestCaseData(new Fraction(5, 4), Fraction.NaN).Returns(1);
            yield return new TestCaseData(Fraction.One, Fraction.NaN).Returns(1);
            yield return new TestCaseData(new Fraction(4, 5), Fraction.NaN).Returns(1);
            yield return new TestCaseData(Fraction.Zero, Fraction.NaN).Returns(1);
            yield return new TestCaseData(Fraction.MinusOne, Fraction.NaN).Returns(1);
            yield return new TestCaseData(new Fraction(-5, 4), Fraction.NaN).Returns(1);
            yield return new TestCaseData(new Fraction(-4, 5), Fraction.NaN).Returns(1);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.NaN).Returns(1);
            // Any number with PositiveInfinity
            yield return new TestCaseData(Fraction.One, Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(Fraction.MinusOne, Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(new Fraction(42, 66, false), Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(new Fraction(-42, 66, false), Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(new Fraction(42, -66, false), Fraction.PositiveInfinity).Returns(-1);
            yield return new TestCaseData(new Fraction(-42, -66, false), Fraction.PositiveInfinity).Returns(-1);
            // Any number with NegativeInfinity
            yield return new TestCaseData(Fraction.One, Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(Fraction.MinusOne, Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(new Fraction(42, 66, false), Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(new Fraction(-42, 66, false), Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(new Fraction(42, -66, false), Fraction.NegativeInfinity).Returns(1);
            yield return new TestCaseData(new Fraction(-42, -66, false), Fraction.NegativeInfinity).Returns(1);
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public int The_CompareTo_method_should_return_the_expected_result(Fraction a, Fraction b) {
        return a.CompareTo(b);
    }


    private static IEnumerable<TestCaseData> DifferentSignsTestCases {
        get {
            #region {positive/positive} and {positive/negative}

            // {1/1} != {1/-10}
            yield return new TestCaseData(new Fraction(1, 1, false), new Fraction(1, -10, false)).Returns(1);
            // {1/10} != {1/-1}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(1, -1, false)).Returns(1);

            #endregion

            #region {positive/positive} and {negative/positive}

            // {1/1} != {-1/10}
            yield return new TestCaseData(new Fraction(1, 1, false), new Fraction(-1, 10, false)).Returns(1);
            // {1/10} != {-1/1}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(-1, 1, false)).Returns(1);

            #endregion

            #region {positive/negative} and {positive/positive}

            // {1/-1} != {1/10}
            yield return new TestCaseData(new Fraction(1, -1, false), new Fraction(1, 10, false)).Returns(-1);
            // {1/-10} != {1/1}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(1, 1, false)).Returns(-1);

            #endregion

            #region {negative/positive} and {positive/positive}

            // {-1/1} != {1/10}
            yield return new TestCaseData(new Fraction(-1, 1, false), new Fraction(1, 10, false)).Returns(-1);
            // {-1/10} != {1/1}
            yield return new TestCaseData(new Fraction(-1, 10, false), new Fraction(1, 1, false)).Returns(-1);

            #endregion

            #region {negative/negative} and {negative/positive}

            // {-1/-1} != {-1/10}
            yield return new TestCaseData(new Fraction(-1, -1, false), new Fraction(-1, 10, false)).Returns(1);
            // {-1/-10} != {-1/1}
            yield return new TestCaseData(new Fraction(-1, -10, false), new Fraction(-1, 1, false)).Returns(1);

            #endregion

            #region {negative/negative} and {positive/negative}

            // {-1/-1} != {1/-10}
            yield return new TestCaseData(new Fraction(-1, -1, false), new Fraction(1, -10, false)).Returns(1);
            // {-1/-10} != {1/-1}
            yield return new TestCaseData(new Fraction(-1, -10, false), new Fraction(1, -1, false)).Returns(1);

            #endregion

            #region {negative/positive} and {negative/negative}

            // {-1/1} != {-1/-10}
            yield return new TestCaseData(new Fraction(-1, 1, false), new Fraction(-1, -10, false)).Returns(-1);
            // {-1/10} != {-1/-1}
            yield return new TestCaseData(new Fraction(-1, 10, false), new Fraction(-1, -1, false)).Returns(-1);

            #endregion

            #region {positive/negative} and {negative/negative}

            // {1/-1} != {-1/-10}
            yield return new TestCaseData(new Fraction(1, -1, false), new Fraction(-1, -10, false)).Returns(-1);
            // {1/-10} != {-1/-1}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(-1, -1, false)).Returns(-1);

            #endregion
        }
    }

    [Test, TestCaseSource(nameof(DifferentSignsTestCases))]
    public int Fractions_with_different_signs_should_return_non_zero(Fraction a, Fraction b) => a.CompareTo(b);


    private static IEnumerable<TestCaseData> EqualFractionsTestCases {
        get {
            #region {positive/positive} and {positive/positive}

            // {10/10} == {1/1}
            yield return new TestCaseData(new Fraction(10, 10, false), new Fraction(1, 1, false)).Returns(0);
            // {10/100} == {1/10}
            yield return new TestCaseData(new Fraction(10, 100, false), new Fraction(1, 10, false)).Returns(0);

            #endregion

            #region {negative/negative} and {negative/negative}

            // {-10/-10} == {-1/-1}
            yield return new TestCaseData(new Fraction(-10, -10, false), new Fraction(-1, -1, false)).Returns(0);
            // {-10/-100} == {-1/-10}
            yield return new TestCaseData(new Fraction(-10, -100, false), new Fraction(-1, -10, false)).Returns(0);

            #endregion

            #region {positive/positive} and {negative/negative}

            // {10/10} == {-1/-1} 
            yield return new TestCaseData(new Fraction(10, 10, false), new Fraction(-1, -1, false)).Returns(0);
            // {1/10} == {-10/-100}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(-10, -100, false)).Returns(0);
            // {10/100} == {-1/-10}
            yield return new TestCaseData(new Fraction(10, 100, false), new Fraction(-1, -10, false)).Returns(0);

            #endregion

            #region {negative/negative} and {positive/positive}

            // {-10/-10} == {1/1} 
            yield return new TestCaseData(new Fraction(-10, -10, false), new Fraction(1, 1, false)).Returns(0);
            // {-1/-10} == {10/100}
            yield return new TestCaseData(new Fraction(-1, -10, false), new Fraction(10, 100, false)).Returns(0);

            #endregion

            #region {positive/negative} and {positive/negative}

            // {10/-10} == {1/-1} 
            yield return new TestCaseData(new Fraction(10, -10, false), new Fraction(1, -1, false)).Returns(0);
            // {1/-10} == {10/-100}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(10, -100, false)).Returns(0);

            #endregion

            #region {positive/negative} and {negative/positive}

            // {10/-10} == {-1/1} 
            yield return new TestCaseData(new Fraction(10, -10, false), new Fraction(-1, 1, false)).Returns(0);
            // {1/-10} == {-10/100}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(-10, 100, false)).Returns(0);

            #endregion

            #region {negative/positive} and {positive/negative}

            // {-10/10} == {1/-1} 
            yield return new TestCaseData(new Fraction(-10, 10, false), new Fraction(1, -1, false)).Returns(0);
            // {-1/10} and {10/-100}
            yield return new TestCaseData(new Fraction(-1, 10, false), new Fraction(10, -100, false)).Returns(0);
            // {-10/100} == {1/-10}
            yield return new TestCaseData(new Fraction(-10, 100, false), new Fraction(1, -10, false)).Returns(0);

            #endregion

            #region {negative/positive} and {negative/positive}

            // {-10/10} == {-1/1} 
            yield return new TestCaseData(new Fraction(-10, 10, false), new Fraction(-1, 1, false)).Returns(0);
            // {-10/100} == {-1/10}
            yield return new TestCaseData(new Fraction(-10, 100, false), new Fraction(-1, 10, false)).Returns(0);

            #endregion
        }
    }

    [Test, TestCaseSource(nameof(EqualFractionsTestCases))]
    public int Equal_fractions_should_return_zero(Fraction a, Fraction b) => a.CompareTo(b);


    private static IEnumerable<TestCaseData> DifferentFractionsTestCases {
        get {
            #region {positive/positive} and {positive/positive}

            // {1/2} < {2/2}
            yield return new TestCaseData(new Fraction(1, 2, false), new Fraction(2, 2, false)).Returns(-1);
            // {11/10} > {1/1}
            yield return new TestCaseData(new Fraction(11, 10, false), new Fraction(1, 1, false)).Returns(1);
            // {10/10} > {1/2}
            yield return new TestCaseData(new Fraction(10, 10, false), new Fraction(1, 2, false)).Returns(1);
            // {10/10} < {2/1}
            yield return new TestCaseData(new Fraction(10, 10, false), new Fraction(2, 1, false)).Returns(-1);
            // {1/10} < {1/1}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(1, 1, false)).Returns(-1);
            // {10/100} < {2/10}
            yield return new TestCaseData(new Fraction(10, 100, false), new Fraction(2, 10, false)).Returns(-1);
            // {7/4} > {3/2}
            yield return new TestCaseData(new Fraction(7, 4, false), new Fraction(3, 2, false)).Returns(1);
            // {6/5} < {3/2}
            yield return new TestCaseData(new Fraction(6, 5, false), new Fraction(3, 2, false)).Returns(-1);

            #endregion

            #region {negative/negative} and {negative/negative}

            // {-1/-2} < {-2/-2}
            yield return new TestCaseData(new Fraction(-1, -2, false), new Fraction(-2, -2, false)).Returns(-1);
            // {-1/-2} < {-10/-10}
            yield return new TestCaseData(new Fraction(-1, -2, false), new Fraction(-10, -10, false)).Returns(-1);
            // {-1/-1} < {-11/-10}
            yield return new TestCaseData(new Fraction(-1, -1, false), new Fraction(-11, -10, false)).Returns(-1);
            // {-10/-10} < {-2/-1}
            yield return new TestCaseData(new Fraction(-10, -10, false), new Fraction(-2, -1, false)).Returns(-1);
            // {-1/-10} < {-1/-1}
            yield return new TestCaseData(new Fraction(-1, -10, false), new Fraction(-1, -1, false)).Returns(-1);
            // {-10/-100} < {-2/-10}
            yield return new TestCaseData(new Fraction(-10, -100, false), new Fraction(-2, -10, false)).Returns(-1);
            // {-7/-4} > {-3/-2}
            yield return new TestCaseData(new Fraction(-7, -4, false), new Fraction(-3, -2, false)).Returns(1);
            // {-6/-5} < {-3/-2}
            yield return new TestCaseData(new Fraction(-6, -5, false), new Fraction(-3, -2, false)).Returns(-1);

            #endregion

            #region {positive/positive} and {negative/negative}

            // {1/2} < {-2/-2}
            yield return new TestCaseData(new Fraction(1, 2, false), new Fraction(-2, -2, false)).Returns(-1);
            // {1/10} < {-11/-100}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(-11, -100, false)).Returns(-1);
            // {1/1} > {-2/-10}
            yield return new TestCaseData(new Fraction(1, 1, false), new Fraction(-2, -10, false)).Returns(1);
            // {1/1} > {-1/-2}
            yield return new TestCaseData(new Fraction(1, 1, false), new Fraction(-1, -2, false)).Returns(1);
            // {2/1} > {-1/-2}
            yield return new TestCaseData(new Fraction(2, 1, false), new Fraction(-1, -2, false)).Returns(1);
            // {10/10} < {-2/-1} 
            yield return new TestCaseData(new Fraction(10, 10, false), new Fraction(-2, -1, false)).Returns(-1);
            // {1/10} < {-1/-1}
            yield return new TestCaseData(new Fraction(1, 10, false), new Fraction(-1, -1, false)).Returns(-1);
            // {10/100} < {-2/-10}
            yield return new TestCaseData(new Fraction(10, 100, false), new Fraction(-2, -10, false)).Returns(-1);
            // {7/4} > {-3/-2}
            yield return new TestCaseData(new Fraction(7, 4, false), new Fraction(-3, -2, false)).Returns(1);
            // {6/5} < {-3/-2}
            yield return new TestCaseData(new Fraction(6, 5, false), new Fraction(-3, -2, false)).Returns(-1);

            #endregion

            #region {negative/negative} and {positive/positive}

            // {-1/-2} < {2/2}
            yield return new TestCaseData(new Fraction(-1, -2, false), new Fraction(2, 2, false)).Returns(-1);
            // {-10/-10} < {2/1} 
            yield return new TestCaseData(new Fraction(-10, -10, false), new Fraction(2, 1, false)).Returns(-1);
            // {-1/-10} < {1/1}
            yield return new TestCaseData(new Fraction(-1, -10, false), new Fraction(1, 1, false)).Returns(-1);
            // {-10/-100} < {2/10}
            yield return new TestCaseData(new Fraction(-10, -100, false), new Fraction(2, 10, false)).Returns(-1);
            // {-7/-4} > {3/2}
            yield return new TestCaseData(new Fraction(-7, -4, false), new Fraction(3, 2, false)).Returns(1);
            // {-6/-5} < {3/2}
            yield return new TestCaseData(new Fraction(-6, -5, false), new Fraction(3, 2, false)).Returns(-1);

            #endregion

            #region {positive/negative} and {positive/negative}

            // {1/-2} > {2/-2}
            yield return new TestCaseData(new Fraction(1, -2, false), new Fraction(2, -2, false)).Returns(1);
            // {1/-10} > {11/-100}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(11, -100, false)).Returns(1);
            // {10/-10} > {2/-1} 
            yield return new TestCaseData(new Fraction(10, -10, false), new Fraction(2, -1, false)).Returns(1);
            // {1/-10} > {1/-1}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(1, -1, false)).Returns(1);
            // {10/-100} > {2/-10}
            yield return new TestCaseData(new Fraction(10, -100, false), new Fraction(2, -10, false)).Returns(1);
            // {7/-4} < {3/-2}
            yield return new TestCaseData(new Fraction(7, -4, false), new Fraction(3, -2, false)).Returns(-1);
            // {6/-5} > {3/-2}
            yield return new TestCaseData(new Fraction(6, -5, false), new Fraction(3, -2, false)).Returns(1);

            #endregion

            #region {positive/negative} and {negative/positive}

            // {1/-2} > {-2/2}
            yield return new TestCaseData(new Fraction(1, -2, false), new Fraction(-2, 2, false)).Returns(1);
            // {10/-10} > {-2/1} 
            yield return new TestCaseData(new Fraction(10, -10, false), new Fraction(-2, 1, false)).Returns(1);
            // {1/-10} > {-1/1}
            yield return new TestCaseData(new Fraction(1, -10, false), new Fraction(-1, 1, false)).Returns(1);
            // {10/-100} > {-2/10}
            yield return new TestCaseData(new Fraction(10, -100, false), new Fraction(-2, 10, false)).Returns(1);
            // {7/-4} > {-3/2}
            yield return new TestCaseData(new Fraction(7, -4, false), new Fraction(-3, 2, false)).Returns(-1);
            // {6/-5} > {-3/2}
            yield return new TestCaseData(new Fraction(6, -5, false), new Fraction(-3, 2, false)).Returns(1);

            #endregion

            #region {negative/positive} and {positive/negative}

            // {-1/2} > {2/-2}
            yield return new TestCaseData(new Fraction(-1, 2, false), new Fraction(2, -2, false)).Returns(1);
            // {-1/1} > {2/-1} 
            yield return new TestCaseData(new Fraction(-1, 1, false), new Fraction(2, -1, false)).Returns(1);
            // {-1/2} > {2/-1} 
            yield return new TestCaseData(new Fraction(-1, 2, false), new Fraction(2, -1, false)).Returns(1);
            // {-2/1} > {3/-1} 
            yield return new TestCaseData(new Fraction(-2, 1, false), new Fraction(3, -1, false)).Returns(1);
            // {-11/10} < {1/-1} 
            yield return new TestCaseData(new Fraction(-11, 10, false), new Fraction(1, -1, false)).Returns(-1);
            // {-10/10} > {2/-1} 
            yield return new TestCaseData(new Fraction(-10, 10, false), new Fraction(2, -1, false)).Returns(1);
            // {-1/10} > {1/-1}
            yield return new TestCaseData(new Fraction(-1, 10, false), new Fraction(1, -1, false)).Returns(1);
            // {-10/100} > {2/-10}
            yield return new TestCaseData(new Fraction(-10, 100, false), new Fraction(2, -10, false)).Returns(1);
            // {-7/4} > {3/-2}
            yield return new TestCaseData(new Fraction(-7, 4, false), new Fraction(3, -2, false)).Returns(-1);
            // {-6/5} > {3/-2}
            yield return new TestCaseData(new Fraction(-6, 5, false), new Fraction(3, -2, false)).Returns(1);

            #endregion

            #region {negative/positive} and {negative/positive}

            // {-1/2} > {-2/2}
            yield return new TestCaseData(new Fraction(-1, 2, false), new Fraction(-2, 2, false)).Returns(1);
            // {-11/10} < {-1/1} 
            yield return new TestCaseData(new Fraction(-11, 10, false), new Fraction(-1, 1, false)).Returns(-1);
            // {-10/10} < {-1/2} 
            yield return new TestCaseData(new Fraction(-10, 10, false), new Fraction(-1, 2, false)).Returns(-1);
            // {-10/10} > {-2/1} 
            yield return new TestCaseData(new Fraction(-10, 10, false), new Fraction(-2, 1, false)).Returns(1);
            // {-1/10} > {-1/1}
            yield return new TestCaseData(new Fraction(-1, 10, false), new Fraction(-1, 1, false)).Returns(1);
            // {-10/100} > {-2/10}
            yield return new TestCaseData(new Fraction(-10, 100, false), new Fraction(-2, 10, false)).Returns(1);
            // {-7/4} > {-3/2}
            yield return new TestCaseData(new Fraction(-7, 4, false), new Fraction(-3, 2, false)).Returns(-1);
            // {-6/5} > {-3/2}
            yield return new TestCaseData(new Fraction(-6, 5, false), new Fraction(-3, 2, false)).Returns(1);

            #endregion
        }
    }

    [Test, TestCaseSource(nameof(DifferentFractionsTestCases))]
    public int Different_Fractions_with_same_signs_should_return_non_zero(Fraction a, Fraction b) => a.CompareTo(b);

    public static IEnumerable<Fraction> FractionsToSort => [
        Fraction.Zero,
        Fraction.One,
        10,
        new Fraction(1, 10),
        new Fraction(0.135m),
        new Fraction(-0.135m),
        new Fraction(decimal.MaxValue),
        new Fraction(decimal.MinValue),
        new Fraction(BigInteger.Pow(-10, 37)),
        new Fraction(1, BigInteger.Pow(10, 12)),
        new Fraction(42, 66, false),
        new Fraction(36, 96, false),
        new Fraction(42, -96, false),
        new Fraction(-42, -96, false),
        new Fraction(-42, 96, false),
        Fraction.FromDouble(Math.PI),
        Fraction.FromDouble(-Math.PI),
        Fraction.NaN,
        Fraction.PositiveInfinity,
        Fraction.NegativeInfinity
    ];


    [Test]
    public void Should_sort_fractions_in_expected_order() {
        // Arrange
        var result = FractionsToSort.OrderBy(fraction => fraction).ToArray();
        // Assert
        result.Should().BeInAscendingOrder((a, b) => a.ToDouble().CompareTo(b.ToDouble()));
    }

    [Test]
    public void Should_sort_fractions_in_expected_descending_order() {
        // Act
        var result = FractionsToSort.OrderByDescending(fraction => fraction).ToArray();
        // Assert
        result.Should().BeInDescendingOrder((a, b) => a.ToDouble().CompareTo(b.ToDouble()));
    }

    [Test]
    public void The_CompareTo_method_should_return_1_if_the_other_is_null() {
        Assert.That(Fraction.One.CompareTo(null), Is.EqualTo(1));
        Assert.That(Fraction.MinusOne.CompareTo(null), Is.EqualTo(1));
    }

    [Test]
    public void The_CompareTo_method_should_throw_ArgumentException_if_given_another_type() {
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        Invoking(() => Fraction.One.CompareTo("string")).Should().Throw<ArgumentException>();
    }

    [Test]
    public void The_CompareTo_method_given_a_valid_object_should_return_the_expected_result() {
        object minusOne = Fraction.MinusOne;
        Assert.That(Fraction.Zero.CompareTo(minusOne), Is.EqualTo(1));
        Assert.That(Fraction.MinusOne.CompareTo(minusOne), Is.EqualTo(0));
    }
}
