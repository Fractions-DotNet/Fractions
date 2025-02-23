#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsPositive : Spec {
    [Test]
    public void It_should_return_true_for_a_positive_fraction() {
        IsPositive(new Fraction(1, 3)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_a_negative_fraction() {
        IsPositive(new Fraction(-1, 3)).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_Zero() {
        IsPositive(Fraction.Zero).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_PositiveInfinity() {
        IsPositive(Fraction.PositiveInfinity).Should().Be(true);
        IsPositive(double.PositiveInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_NegativeInfinity() {
        IsPositive(Fraction.NegativeInfinity).Should().Be(false);
        IsPositive(double.NegativeInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsPositive(Fraction.NaN).Should().Be(false);
        IsPositive(double.NaN).Should().Be(false);
    }

    private static bool IsPositive<T>(T value)
        where T : INumberBase<T> {
        return T.IsPositive(value);
    }
}
#endif