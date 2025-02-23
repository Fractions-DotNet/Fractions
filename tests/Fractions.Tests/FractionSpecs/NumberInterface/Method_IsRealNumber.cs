#if NET
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.NumberInterface;

[TestFixture]
[TestOf(typeof(Fraction))]
public class When_calling_IsRealNumber : Spec {
    [Test]
    public void It_should_return_true_for_a_finite_fraction() {
        IsRealNumber(new Fraction(1, 3)).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_PositiveInfinity() {
        IsRealNumber(Fraction.PositiveInfinity).Should().Be(true);
        IsRealNumber(double.PositiveInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_true_for_NegativeInfinity() {
        IsRealNumber(Fraction.NegativeInfinity).Should().Be(true);
        IsRealNumber(double.NegativeInfinity).Should().Be(true);
    }

    [Test]
    public void It_should_return_false_for_NaN() {
        IsRealNumber(Fraction.NaN).Should().Be(false);
        IsRealNumber(double.NaN).Should().Be(false);
    }

    private static bool IsRealNumber<T>(T value)
        where T : INumberBase<T> {
        return T.IsRealNumber(value);
    }
}
#endif
