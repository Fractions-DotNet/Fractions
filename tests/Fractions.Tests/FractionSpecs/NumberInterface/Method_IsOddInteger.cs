#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsOddInteger : Spec {
    [Test]
    public void It_should_return_false_for_fractional_values() {
        IsOddInteger(new Fraction(1, 3)).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_even_integers() {
        IsOddInteger(new Fraction(4, 1)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_odd_integers() {
        IsOddInteger(new Fraction(3, 1)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_non_reduced_odd_integers() {
        IsOddInteger(new Fraction(9, 3, false)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_PositiveInfinity() {
        IsOddInteger(Fraction.PositiveInfinity).Should().Be(false);
        IsOddInteger(double.PositiveInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NegativeInfinity() {
        IsOddInteger(Fraction.NegativeInfinity).Should().Be(false);
        IsOddInteger(double.NegativeInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsOddInteger(Fraction.NaN).Should().Be(false);
        IsOddInteger(double.NaN).Should().Be(false);
    }

    private static bool IsOddInteger<T>(T value)
        where T : INumberBase<T> {
        return T.IsOddInteger(value);
    }
}
#endif
