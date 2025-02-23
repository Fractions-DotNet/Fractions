#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsNegative : Spec {
    [Test]
    public void It_should_return_false_for_a_positive_fraction() {
        IsNegative(new Fraction(1, 3)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_a_negative_fraction() {
        IsNegative(new Fraction(-1, 3)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_Zero() {
        IsNegative(Fraction.Zero).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_PositiveInfinity() {
        IsNegative(Fraction.PositiveInfinity).Should().Be(false);
        IsNegative(double.PositiveInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_NegativeInfinity() {
        IsNegative(Fraction.NegativeInfinity).Should().Be(true);
        IsNegative(double.NegativeInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsNegative(Fraction.NaN).Should().Be(false);
        IsNegative(double.NaN).Should().Be(true, "It checks the sign bit of the floating-point representation");
    }

    private static bool IsNegative<T>(T value)
        where T : INumberBase<T> {
        return T.IsNegative(value);
    }
}
#endif