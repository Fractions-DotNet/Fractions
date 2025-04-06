#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsInteger : Spec {
    [Test]
    public void It_should_return_false_for_fractional_values() {
        IsInteger(new Fraction(1, 3)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_even_integers() {
        IsInteger(new Fraction(4, 1)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_odd_integers() {
        IsInteger(new Fraction(3, 1)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_non_reduced_zero() {
        IsInteger(new Fraction(0, 10, false)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_non_reduced_integers() {
        IsInteger(new Fraction(4, 2, false)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_PositiveInfinity() {
        IsInteger(Fraction.PositiveInfinity).Should().Be(false);
        IsInteger(double.PositiveInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NegativeInfinity() {
        IsInteger(Fraction.NegativeInfinity).Should().Be(false);
        IsInteger(double.NegativeInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsInteger(Fraction.NaN).Should().Be(false);
        IsInteger(double.NaN).Should().Be(false);
    }

    private static bool IsInteger<T>(T value)
        where T : INumberBase<T> {
        return T.IsInteger(value);
    }
}
#endif
