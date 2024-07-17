using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using FluentAssertions;
using Fractions.Formatter;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.ToString;

[TestFixture]
[SetUICulture("en-US")]
[SetCulture("en-US")]
public class When_the_user_calls_ToString_without_any_parameters_having_en_US_culture : Spec {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            var en = CultureInfo.GetCultureInfo("en-US");

            yield return new TestCaseData(new BigInteger(3), new BigInteger(0), true)
                .Returns(en.NumberFormat.PositiveInfinitySymbol);
            yield return new TestCaseData(new BigInteger(3), new BigInteger(0), false)
                .Returns("3/0");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(0), true)
                .Returns(en.NumberFormat.NaNSymbol);
            yield return new TestCaseData(new BigInteger(0), new BigInteger(3), true)
                .Returns("0");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(3), false)
                .Returns("0/3");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(-3), false)
                .Returns("0/-3");
            yield return new TestCaseData(new BigInteger(1), new BigInteger(3), true)
                .Returns("1/3");
            yield return new TestCaseData(new BigInteger(1), new BigInteger(3000000), true)
                .Returns("1/3000000");
            yield return new TestCaseData(new BigInteger(300000), new BigInteger(1), true)
                .Returns("300000");
            yield return new TestCaseData(new BigInteger(-1), new BigInteger(3), true)
                .Returns("-1/3");
            yield return new TestCaseData(new BigInteger(1), new BigInteger(-3), false)
                .Returns("1/-3");
            yield return new TestCaseData(new BigInteger(100000), new BigInteger(-3), false)
                .Returns("100000/-3");
            yield return new TestCaseData(new BigInteger(-1), new BigInteger(-3), false)
                .Returns("-1/-3");
            yield return new TestCaseData(new BigInteger(-1), new BigInteger(-300000), false)
                .Returns("-1/-300000");
            yield return new TestCaseData(new BigInteger(-3), new BigInteger(0), true)
                .Returns(en.NumberFormat.NegativeInfinitySymbol);
            yield return new TestCaseData(new BigInteger(-3), new BigInteger(0), false)
                .Returns("-3/0");
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public string The_text_output_should_be_the_expected_one(BigInteger numerator, BigInteger denominator,
        bool normalize) {
        return new Fraction(numerator, denominator, normalize).ToString();
    }
}

[TestFixture]
public class When_the_user_calls_ToString_using_invariant_culture : Spec {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            yield return new TestCaseData(new BigInteger(3), new BigInteger(0), true)
                .Returns(CultureInfo.InvariantCulture.NumberFormat.PositiveInfinitySymbol);
            yield return new TestCaseData(new BigInteger(3), new BigInteger(0), false)
                .Returns("3/0");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(0), true)
                .Returns(CultureInfo.InvariantCulture.NumberFormat.NaNSymbol);
            yield return new TestCaseData(new BigInteger(0), new BigInteger(3), true)
                .Returns("0");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(3), false)
                .Returns("0/3");
            yield return new TestCaseData(new BigInteger(0), new BigInteger(-3), false)
                .Returns("0/-3");
            yield return new TestCaseData(new BigInteger(1), new BigInteger(3), true)
                .Returns("1/3");
            yield return new TestCaseData(new BigInteger(-1), new BigInteger(3), true)
                .Returns("-1/3");
            yield return new TestCaseData(new BigInteger(1), new BigInteger(-3), false)
                .Returns("1/-3");
            yield return new TestCaseData(new BigInteger(-1), new BigInteger(-3), false)
                .Returns("-1/-3");
            yield return new TestCaseData(new BigInteger(-3), new BigInteger(0), true)
                .Returns(CultureInfo.InvariantCulture.NumberFormat.NegativeInfinitySymbol);
            yield return new TestCaseData(new BigInteger(-3), new BigInteger(0), false)
                .Returns("-3/0");
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public string The_text_output_should_be_the_expected_one(BigInteger numerator, BigInteger denominator,
        bool normalize) {
        return new Fraction(numerator, denominator, normalize).ToString(CultureInfo.InvariantCulture);
    }
}

[TestFixture]
public class When_the_user_calls_ToString_using_a_format_string : Spec {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            var de = CultureInfo.GetCultureInfo("de-DE");
            var en = CultureInfo.GetCultureInfo("en-US");
            var invariant = CultureInfo.InvariantCulture;

            yield return new TestCaseData(new Fraction(1, 3), string.Empty, en)
                .Returns("1/3");
            yield return new TestCaseData(new Fraction(1, 3), null, en)
                .Returns("1/3");
            yield return new TestCaseData(Fraction.Zero, null, en)
                .Returns("0");

            yield return new TestCaseData(new Fraction(0, 0), null, en)
                .Returns(en.NumberFormat.NaNSymbol);

            yield return new TestCaseData(new Fraction(1, 0), null, en)
                .Returns(en.NumberFormat.PositiveInfinitySymbol);

            yield return new TestCaseData(new Fraction(-1, 0), null, en)
                .Returns(en.NumberFormat.NegativeInfinitySymbol);

            yield return new TestCaseData(new Fraction(0, 0), null, de)
                .Returns(de.NumberFormat.NaNSymbol);

            yield return new TestCaseData(new Fraction(1, 0), null, de)
                .Returns(de.NumberFormat.PositiveInfinitySymbol);

            yield return new TestCaseData(new Fraction(-1, 0), null, de)
                .Returns(de.NumberFormat.NegativeInfinitySymbol);

            yield return new TestCaseData(new Fraction(0, 0), null, invariant)
                .Returns(invariant.NumberFormat.NaNSymbol);

            yield return new TestCaseData(new Fraction(1, 0), null, invariant)
                .Returns(invariant.NumberFormat.PositiveInfinitySymbol);

            yield return new TestCaseData(new Fraction(-1, 0), null, invariant)
                .Returns(invariant.NumberFormat.NegativeInfinitySymbol);

            yield return new TestCaseData(new Fraction(0, 3), "G", de)
                .Returns("0");
            yield return new TestCaseData(new Fraction(1, 3), "G", de)
                .Returns("1/3");
            yield return new TestCaseData(new Fraction(3, 1), "G", de)
                .Returns("3");
            yield return new TestCaseData(new Fraction(1, 3), "G", new DefaultFractionFormatProvider())
                .Returns("1/3");
            yield return new TestCaseData(new Fraction(1, 3), "G", en)
                .Returns("1/3");
            yield return new TestCaseData(new Fraction(-1, 3), "G", en)
                .Returns("-1/3");
            yield return new TestCaseData(new Fraction(1, -3, false), "G", en)
                .Returns("1/-3");
            yield return new TestCaseData(new Fraction(-1, -3, false), "G", en)
                .Returns("-1/-3");
            yield return new TestCaseData(Fraction.Zero, "G", en)
                .Returns("0");

            yield return new TestCaseData(new Fraction(-1, -3, false), "n", en)
                .Returns("-1");
            yield return new TestCaseData(new Fraction(3, 1), "n", de)
                .Returns("3");

            yield return new TestCaseData(new Fraction(-1, -3, false), "d", en)
                .Returns("-3");
            yield return new TestCaseData(new Fraction(-1, -3, false), "d d", en)
                .Returns("-3 -3");
            yield return new TestCaseData(new Fraction(3, 1), "d", de)
                .Returns("1");
            yield return new TestCaseData(Fraction.Zero, "d", de)
                .Returns("1");

            yield return new TestCaseData(new Fraction(1, 3), "n/d", en)
                .Returns("1/3");
            yield return new TestCaseData(Fraction.Zero, "n/d", en)
                .Returns("0/1");

            yield return new TestCaseData(new Fraction(3, 1), "z", de)
                .Returns("3");
            yield return new TestCaseData(new Fraction(1, 3), "z", de)
                .Returns("0");
            yield return new TestCaseData(new Fraction(1, 2), "z", de)
                .Returns("0");
            yield return new TestCaseData(Fraction.Zero, "z", de)
                .Returns("0");
            yield return new TestCaseData(new Fraction(1, 2), "r", de)
                .Returns("1/2");
            yield return new TestCaseData(new Fraction(1, -2), "r", de)
                .Returns("-1/2");
            yield return new TestCaseData(Fraction.Zero, "r", de)
                .Returns("0");
            yield return new TestCaseData(Fraction.NaN, "r", de)
                .Returns(string.Empty);
            yield return new TestCaseData(Fraction.PositiveInfinity, "r", de)
                .Returns(string.Empty);
            yield return new TestCaseData(Fraction.NegativeInfinity, "r", de)
                .Returns(string.Empty);
            yield return new TestCaseData(new Fraction(0, 0, normalize: false), "r", de)
                .Returns(string.Empty);
            yield return new TestCaseData(new Fraction(1, 0, normalize: false), "r", de)
                .Returns(string.Empty);
            yield return new TestCaseData(new Fraction(-1, 0, normalize: false), "r", de)
                .Returns(string.Empty);

            yield return new TestCaseData(new Fraction(3, 2), "m", de)
                .Returns("1 1/2");
            yield return new TestCaseData(new Fraction(-3, 2), "m", de)
                .Returns("-1 1/2");
            yield return new TestCaseData(new Fraction(-1, 2), "m", de)
                .Returns("-1/2");
            yield return new TestCaseData(new Fraction(1, 2), "m", de)
                .Returns("1/2");
            yield return new TestCaseData(new Fraction(2, 1), "m", de)
                .Returns("2");
            yield return new TestCaseData(new Fraction(-2, 1), "m", de)
                .Returns("-2");
            yield return new TestCaseData(Fraction.Zero, "m", de)
                .Returns("0");
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public string The_text_output_should_be_the_expected_one(Fraction fraction, string format,
        IFormatProvider cultureInfo) {
        return fraction.ToString(format, cultureInfo);
    }

    #if NET
    
    [Test]
    [TestCaseSource(nameof(TestCases))]
    public string The_text_output_when_using_try_format_should_be_the_expected_one(Fraction fraction, string format,
        IFormatProvider cultureInfo) {
        // Arrange
        Span<char> resultSpan = stackalloc char[20];
        // Act
        var succeeded = fraction.TryFormat(resultSpan, out var charsWritten, format.AsSpan(), cultureInfo);
        var convertedString = resultSpan[..charsWritten].ToString();
        // Assert
        succeeded.Should().Imply(!string.IsNullOrEmpty(convertedString));
        charsWritten.Should().Be(convertedString.Length);
        return convertedString;
    }
    
    [Test]
    public void The_result_of_try_format_is_false_when_the_span_size_is_smaller_than_the_expected_result() {
        // Arrange
        var fraction = new Fraction(1, 2);
        Span<char> resultSpan = stackalloc char[2];
        // Act
        var succeeded = fraction.TryFormat(resultSpan, out var charsWritten, null, CultureInfo.InvariantCulture);
        // Assert
        succeeded.Should().BeFalse();
        charsWritten.Should().Be(0);
    }

    #endif
}
