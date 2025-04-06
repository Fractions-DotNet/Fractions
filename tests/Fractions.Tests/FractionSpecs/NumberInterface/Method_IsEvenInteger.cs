#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsEvenInteger : Spec {
    [Test]
    public void It_should_return_false_for_fractional_values() {
        IsEvenInteger(new Fraction(1, 3)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_even_integers() {
        IsEvenInteger(new Fraction(4, 1)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_odd_integers() {
        IsEvenInteger(new Fraction(3, 1)).Should().Be(false);
    }

    [Test]
    public void It_should_return_true_for_non_reduced_even_integers() {
        IsEvenInteger(new Fraction(4, 2, false)).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_PositiveInfinity() {
        IsEvenInteger(Fraction.PositiveInfinity).Should().Be(false);
        IsEvenInteger(double.PositiveInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NegativeInfinity() {
        IsEvenInteger(Fraction.NegativeInfinity).Should().Be(false);
        IsEvenInteger(double.NegativeInfinity).Should().Be(false);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsEvenInteger(Fraction.NaN).Should().Be(false);
        IsEvenInteger(double.NaN).Should().Be(false);
    }

    private static bool IsEvenInteger<T>(T value)
        where T : INumberBase<T> {
        return T.IsEvenInteger(value);
    }
}
#endif
