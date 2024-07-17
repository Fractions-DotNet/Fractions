using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.Magnitude;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_requesting_the_max_magnitude_of_two_fractions : Spec {
    private static readonly object[] MaxMagnitudeTestCases = [
        new object[] { new Fraction(1, 2), new Fraction(1, 3), new Fraction(1, 2) },
        new object[] { new Fraction(-1, 2), new Fraction(1, 3), new Fraction(-1, 2) },
        new object[] { new Fraction(0), new Fraction(1, 3), new Fraction(1, 3) },
        new object[] { Fraction.NaN, new Fraction(1, 3), Fraction.NaN },
        new object[] { new Fraction(1, 3), Fraction.NaN, Fraction.NaN },
        new object[] { Fraction.NaN, Fraction.PositiveInfinity, Fraction.NaN },
        new object[] { Fraction.PositiveInfinity, Fraction.NaN, Fraction.NaN },
        new object[] { Fraction.NaN, Fraction.NegativeInfinity, Fraction.NaN },
        new object[] { Fraction.NegativeInfinity, Fraction.NaN, Fraction.NaN },
        new object[] { Fraction.PositiveInfinity, Fraction.NegativeInfinity, Fraction.PositiveInfinity },
        new object[] { Fraction.NegativeInfinity, Fraction.PositiveInfinity, Fraction.PositiveInfinity }
    ];

    [TestCaseSource(nameof(MaxMagnitudeTestCases))]
    public void MaxMagnitude_ShouldReturnCorrectResult(Fraction x, Fraction y, Fraction expected) {
#if NET
        double.MaxMagnitude(x.ToDouble(), y.ToDouble()).Should().Be(expected.ToDouble());
#endif
        Fraction.MaxMagnitude(x, y).Should().Be(expected);
    }

    private static readonly object[] MaxMagnitudeNumberTestCases = [
        new object[] { new Fraction(1, 2), new Fraction(1, 3), new Fraction(1, 2) },
        new object[] { new Fraction(-1, 2), new Fraction(1, 3), new Fraction(-1, 2) },
        new object[] { new Fraction(1, 3), new Fraction(-1, 2), new Fraction(-1, 2) },
        new object[] { Fraction.NaN, new Fraction(1, 3), new Fraction(1, 3) },
        new object[] { new Fraction(1, 2), Fraction.NaN, new Fraction(1, 2) },
        new object[] { Fraction.NaN, Fraction.PositiveInfinity, Fraction.PositiveInfinity },
        new object[] { Fraction.PositiveInfinity, Fraction.NaN, Fraction.PositiveInfinity },
        new object[] { Fraction.NaN, Fraction.NegativeInfinity, Fraction.NegativeInfinity },
        new object[] { Fraction.NegativeInfinity, Fraction.NaN, Fraction.NegativeInfinity },
        new object[] { Fraction.PositiveInfinity, Fraction.NegativeInfinity, Fraction.PositiveInfinity },
        new object[] { Fraction.NegativeInfinity, Fraction.PositiveInfinity, Fraction.PositiveInfinity },
        new object[] { Fraction.NaN, Fraction.NaN, Fraction.NaN }
    ];

    [TestCaseSource(nameof(MaxMagnitudeNumberTestCases))]
    public void MaxMagnitudeNumber_ShouldReturnCorrectResult(Fraction x, Fraction y, Fraction expected) {
#if NET
        double.MaxMagnitudeNumber(x.ToDouble(), y.ToDouble()).Should().Be(expected.ToDouble());
#endif
        Fraction.MaxMagnitudeNumber(x, y).Should().Be(expected);
    }
}
