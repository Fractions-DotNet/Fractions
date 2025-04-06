#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsCanonical : Spec {
    [Test]
    public void It_should_return_true_for_reduced_fractional_values() {
        IsCanonical(new Fraction(1, 3)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_non_reduced_fractions() {
        IsCanonical(new Fraction(4, 2, false)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_integers() {
        IsCanonical(new Fraction(4, 1)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_zero() {
        IsCanonical(0).Should().Be(true);
        IsCanonical(Fraction.Zero).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_non_reduced_zero() {
        IsCanonical(new Fraction(0, 10, false)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_PositiveInfinity() {
        IsCanonical(double.PositiveInfinity).Should().Be(true);
        IsCanonical(Fraction.PositiveInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_NegativeInfinity() {
        IsCanonical(double.NegativeInfinity).Should().Be(true);
        IsCanonical(Fraction.NegativeInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_NaN() {
        IsCanonical(double.NaN).Should().Be(true);
        IsCanonical(Fraction.NaN).Should().Be(true);
    }

    private static bool IsCanonical<T>(T value)
        where T : INumberBase<T> {
        return T.IsCanonical(value);
    }
}
#endif
