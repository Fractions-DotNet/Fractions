using System;
using System.Collections;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.ToDataType;

[TestFixture]
// German: Wenn ein 0 Bruch in ein Decimal konvertiert wird
public class When_zero_fraction_is_converted_to_decimal : Spec {
    [Test]
    // German: Das Ergebnis soll 0 sein
    public void Result_should_be_zero() {
        Fraction.Zero.ToDecimal().Should().Be(0);
    }
}

[TestFixture]
// German: Wenn ein 1viertel in ein Decimal konvertiert wird
public class When_one_quarter_is_converted_to_decimal : Spec {
    [Test]
    // German: Das Ergebnis soll 0.25 sein
    public void Result_should_be_0_25() {
        new Fraction(1, 4).ToDecimal().Should().Be(0.25m);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit long MaxValue von Fraction in int konvertiert wird
public class When_fraction_with_long_MaxValue_is_converted_to_int : Spec {
    [Test]
    // German: Das sollte eine OverflowException werfen
    public void Should_throw_an_OverflowException() {
        Invoking(() => new Fraction(long.MaxValue).ToInt32())
            .Should()
            .Throw<OverflowException>();
    }
}

[TestFixture]
public class When_fraction_with_non_decimal_denominator_is_converted_to_decimal : Spec {
    private Fraction _fraction;
    private decimal _result;

    public override void SetUp() {
        var threeNinths = new Fraction(3, 9).ToDecimal();
        var sixNinths = new Fraction(6, 9).ToDecimal();
        _fraction = new Fraction(threeNinths) * new Fraction(sixNinths);
    }

    public override void Act() {
        _result = _fraction.ToDecimal();
    }

    [Test]
    // German: Das Resultat soll dem erwarteten Ergebnis entsprechen
    public void Result_should_match_expected_value() {
        Math.Round(_result, 27).Should().Be(0.222222222222222222222222222m);
    }
}

[TestFixture]
// German: Wenn ein Bruch mit sehr großen Zahlen in Zähler und Nenner in ein Decimal konvertiert wird
public class When_fraction_with_large_numerator_and_denominator_is_converted_to_decimal : Spec {
    private Fraction _fraction;
    private decimal _result;

    public override void SetUp() {
        _fraction = new Fraction(new BigInteger(decimal.MaxValue) * -10, new BigInteger(decimal.MaxValue) * 20, false);
    }

    public override void Act() {
        _result = _fraction.ToDecimal();
    }

    [Test]
    public void Result_should_match_expected_value() {
        _result.Should().Be(-0.5m);
    }
}

[TestFixture]
public class When_user_converts_fraction_to_BigInteger : Spec {
    private static IEnumerable TestCases {
        get {
            yield return new TestCaseData(new Fraction(0)).Returns(new BigInteger(0));
            yield return new TestCaseData(new Fraction(1)).Returns(new BigInteger(1));
            yield return new TestCaseData(new Fraction(-1)).Returns(new BigInteger(-1));
            yield return
                new TestCaseData(new Fraction(new BigInteger(long.MaxValue), new BigInteger(1))).Returns(
                    new BigInteger(long.MaxValue));
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public BigInteger Result_should_be_correct(Fraction value) {
        return value.ToBigInteger();
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public BigInteger Result_should_be_correct_when_using_explicit_cast(Fraction value) {
        return (BigInteger)value;
    }
}

[TestFixture]
public class When_converting_NaN : Spec {
    [Test]
    public void ToDecimal_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToDecimal()).Should().Throw<OverflowException>();
    }
    
    [Test]
    public void ToDecimalSaturating_should_return_Zero() {
        Fraction.NaN.ToDecimalSaturating().Should().Be(0);
    }

    [Test]
    public void ToInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToUInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToUInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToBigInteger_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NaN.ToBigInteger()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToDouble_should_return_NaN() {
        Fraction.NaN.ToDouble().Should().Be(double.NaN);
    }
}

[TestFixture]
public class When_converting_PositiveInfinity : Spec {
    [Test]
    public void ToDecimal_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToDecimal()).Should().Throw<OverflowException>();
    }
    
    [Test]
    public void ToDecimalSaturating_should_return_MaxValue() {
        Fraction.PositiveInfinity.ToDecimalSaturating().Should().Be(decimal.MaxValue);
    }
    
    [Test]
    public void ToInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToUInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToUInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToBigInteger_should_throw_a_OverflowException() {
        Invoking(() => Fraction.PositiveInfinity.ToBigInteger()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToDouble_should_return_PositiveInfinity() {
        Fraction.PositiveInfinity.ToDouble().Should().Be(double.PositiveInfinity);
    }
}

[TestFixture]
public class When_converting_NegativeInfinity : Spec {
    [Test]
    public void ToDecimal_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToDecimal()).Should().Throw<OverflowException>();
    }
    
    [Test]
    public void ToDecimalSaturating_should_return_MaxValue() {
        Fraction.NegativeInfinity.ToDecimalSaturating().Should().Be(decimal.MinValue);
    }

    [Test]
    public void ToInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt32_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToUInt32()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToUInt64_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToUInt64()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToBigInteger_should_throw_a_OverflowException() {
        Invoking(() => Fraction.NegativeInfinity.ToBigInteger()).Should().Throw<OverflowException>();
    }

    [Test]
    public void ToDouble_should_return_NegativeInfinity() {
        Fraction.NegativeInfinity.ToDouble().Should().Be(double.NegativeInfinity);
    }
}

[TestFixture]
public class When_fraction_is_converted_to_double : Spec {
    private static IEnumerable TestCases {
        get {
            var largeNumber = BigInteger.Pow(10, 309); // larger than double.MaxValue
            // Zero cases
            yield return new TestCaseData(Fraction.Zero).Returns(0.0);
            yield return new TestCaseData(new Fraction(0, 10, false)).Returns(0.0);
            yield return new TestCaseData(new Fraction(0, -10, false)).Returns(0.0);
            yield return new TestCaseData(new Fraction(0, largeNumber, false)).Returns(0.0).SetName("0 / largeNumber");
            yield return new TestCaseData(new Fraction(0, -largeNumber, false)).Returns(0.0).SetName("0 / -largeNumber");
            // Positive cases
            yield return new TestCaseData(new Fraction(2)).Returns(2.0);
            yield return new TestCaseData(new Fraction(-2, -1, false)).Returns(2.0);
            yield return new TestCaseData(new Fraction(1, 2, false)).Returns(0.5);
            yield return new TestCaseData(new Fraction(-1, -2, false)).Returns(0.5);
            yield return new TestCaseData(new Fraction(1, 3, false)).Returns(1.0 / 3.0);
            yield return new TestCaseData(new Fraction(-1, -3, false)).Returns(1.0 / 3.0);
            yield return new TestCaseData(new Fraction(largeNumber, BigInteger.One, false)).Returns(double.PositiveInfinity).SetName("largeNumber / 1");
            yield return new TestCaseData(new Fraction(-largeNumber, BigInteger.MinusOne, false)).Returns(double.PositiveInfinity).SetName("-largeNumber / -1");
            yield return new TestCaseData(new Fraction(largeNumber, largeNumber, false)).Returns(1.0).SetName("largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -largeNumber, false)).Returns(1.0).SetName("-largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, largeNumber, false)).Returns(2.0).SetName("2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, -largeNumber, false)).Returns(2.0).SetName("-2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, 2 * largeNumber, false)).Returns(0.5).SetName("largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -2 * largeNumber, false)).Returns(0.5).SetName("-largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, 3 * largeNumber, false)).Returns(2.0 / 3.0).SetName("2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, -2 * largeNumber, false)).Returns(3.0 / 2.0).SetName("-3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber * largeNumber, largeNumber, false)).Returns(double.PositiveInfinity).SetName("largeNumber^2 / largeNumber");
            yield return new TestCaseData(new Fraction(BigInteger.One, largeNumber, false)).Returns(0.0).SetName("1 / largeNumber");
            yield return new TestCaseData(new Fraction(1000, largeNumber, false)).Returns(new Fraction(1000, largeNumber).ToDouble()).SetName("1000 / largeNumber");
            // Negative cases
            yield return new TestCaseData(new Fraction(-2)).Returns(-2.0);
            yield return new TestCaseData(new Fraction(2, -1, false)).Returns(-2.0);
            yield return new TestCaseData(new Fraction(-1, 2, false)).Returns(-0.5);
            yield return new TestCaseData(new Fraction(1, -2, false)).Returns(-0.5);
            yield return new TestCaseData(new Fraction(-1, 3, false)).Returns(-1.0 / 3.0);
            yield return new TestCaseData(new Fraction(1, -3, false)).Returns(-1.0 / 3.0);
            yield return new TestCaseData(new Fraction(largeNumber, BigInteger.MinusOne, false)).Returns(double.NegativeInfinity).SetName("largeNumber / -1");
            yield return new TestCaseData(new Fraction(-largeNumber, BigInteger.One, false)).Returns(double.NegativeInfinity).SetName("-largeNumber / 1");
            yield return new TestCaseData(new Fraction(-largeNumber, largeNumber, false)).Returns(-1.0).SetName("-largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -largeNumber, false)).Returns(-1.0).SetName("largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, largeNumber, false)).Returns(-2.0).SetName("-2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, -largeNumber, false)).Returns(-2.0).SetName("2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, 2 * largeNumber, false)).Returns(-0.5).SetName("-largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -2 * largeNumber, false)).Returns(-0.5).SetName("largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, 3 * largeNumber, false)).Returns(-2.0 / 3.0).SetName("-2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, -2 * largeNumber, false)).Returns(-3.0 / 2.0).SetName("3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber * largeNumber, largeNumber, false)).Returns(double.PositiveInfinity).SetName("largeNumber^2 / largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber * largeNumber, largeNumber, false)).Returns(double.NegativeInfinity).SetName("-largeNumber^2 / largeNumber");
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public double Result_should_match_expected_value(Fraction fraction) {
        return fraction.ToDouble();
    }
}

[TestFixture]
public class When_fraction_is_converted_to_decimal : Spec {
    private static IEnumerable TestCases {
        get {
            var largeNumber = 2 * new BigInteger(decimal.MaxValue);
            // Zero cases
            yield return new TestCaseData(Fraction.Zero).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, 10, false)).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, -10, false)).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, largeNumber, false)).Returns(0.0m).SetName("0 / largeNumber");
            yield return new TestCaseData(new Fraction(0, -largeNumber, false)).Returns(0.0m).SetName("0 / -largeNumber");
            // Positive cases
            yield return new TestCaseData(new Fraction(2)).Returns(2.0m);
            yield return new TestCaseData(new Fraction(-2, -1, false)).Returns(2.0m);
            yield return new TestCaseData(new Fraction(1, 2, false)).Returns(0.5m);
            yield return new TestCaseData(new Fraction(-1, -2, false)).Returns(0.5m);
            yield return new TestCaseData(new Fraction(1, 3, false)).Returns(1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(-1, -3, false)).Returns(1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(largeNumber, largeNumber, false)).Returns(1.0m).SetName("largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -largeNumber, false)).Returns(1.0m).SetName("-largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, largeNumber, false)).Returns(2.0m).SetName("2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, -largeNumber, false)).Returns(2.0m).SetName("-2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, 2 * largeNumber, false)).Returns(0.5m).SetName("largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -2 * largeNumber, false)).Returns(0.5m).SetName("-largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, 2 * largeNumber, false)).Returns(1.5m).SetName("3 * largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, -2 * largeNumber, false)).Returns(1.5m).SetName("-3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, 3 * largeNumber, false)).Returns(2.0m / 3.0m).SetName("2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, -2 * largeNumber, false)).Returns(3.0m / 2.0m).SetName("-3 * largeNumber / -2 * largeNumber");
            // Negative cases
            yield return new TestCaseData(new Fraction(-2)).Returns(-2.0m);
            yield return new TestCaseData(new Fraction(2, -1, false)).Returns(-2.0m);
            yield return new TestCaseData(new Fraction(-1, 2, false)).Returns(-0.5m);
            yield return new TestCaseData(new Fraction(1, -2, false)).Returns(-0.5m);
            yield return new TestCaseData(new Fraction(-1, 3, false)).Returns(-1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(1, -3, false)).Returns(-1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(-largeNumber, largeNumber, false)).Returns(-1.0m).SetName("-largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -largeNumber, false)).Returns(-1.0m).SetName("largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, largeNumber, false)).Returns(-2.0m).SetName("-2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, -largeNumber, false)).Returns(-2.0m).SetName("2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, 2 * largeNumber, false)).Returns(-0.5m).SetName("-largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -2 * largeNumber, false)).Returns(-0.5m).SetName("largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, 2 * largeNumber, false)).Returns(-1.5m).SetName("-3 * largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, -2 * largeNumber, false)).Returns(-1.5m).SetName("3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, 3 * largeNumber, false)).Returns(-2.0m / 3.0m).SetName("-2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, -2 * largeNumber, false)).Returns(-3.0m / 2.0m).SetName("3 * largeNumber / -2 * largeNumber");
        }
    }

    
    [Test]
    [TestCaseSource(nameof(TestCases))]
    public decimal Result_should_match_expected_value(Fraction fraction) {
        return fraction.ToDecimal();
    }
    
    [Test]
    public void An_OverflowException_is_thrown_when_result_is_greater_than_MaxValue() {
        Invoking(() => (2 * new Fraction(decimal.MaxValue)).ToDecimal())
            .Should()
            .Throw<OverflowException>();
    }
    
    [Test]
    public void An_OverflowException_is_thrown_when_result_is_smaller_than_MinValue() {
        Invoking(() => (2 * new Fraction(decimal.MinValue)).ToDecimal())
            .Should()
            .Throw<OverflowException>();
    }

    [Test]
    public void An_OverflowException_is_thrown_when_result_is_positive_and_smaller_than_One_over_MaxValue() {
        Invoking(() => new Fraction(1, 2 * (BigInteger)decimal.MaxValue).ToDecimal())
            .Should()
            .Throw<OverflowException>();
    }

    [Test]
    public void An_OverflowException_is_thrown_when_result_is_negative_and_larger_than_MinusOne_over_MaxValue() {
        Invoking(() => new Fraction(-1, 2 * (BigInteger)decimal.MaxValue).ToDecimal())
            .Should()
            .Throw<OverflowException>();
    }
}

[TestFixture]
public class When_fraction_is_converted_to_decimal_with_saturation : Spec {
    private static IEnumerable TestCases {
        get {
            var largeNumber = 2 * new BigInteger(decimal.MaxValue);
            // Zero cases
            yield return new TestCaseData(Fraction.Zero).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, 10, false)).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, -10, false)).Returns(0.0m);
            yield return new TestCaseData(new Fraction(0, largeNumber, false)).Returns(0.0m).SetName("0 / largeNumber");
            yield return new TestCaseData(new Fraction(0, -largeNumber, false)).Returns(0.0m).SetName("0 / -largeNumber");
            // Positive cases
            yield return new TestCaseData(new Fraction(2)).Returns(2.0m);
            yield return new TestCaseData(new Fraction(-2, -1, false)).Returns(2.0m);
            yield return new TestCaseData(new Fraction(1, 2, false)).Returns(0.5m);
            yield return new TestCaseData(new Fraction(-1, -2, false)).Returns(0.5m);
            yield return new TestCaseData(new Fraction(1, 3, false)).Returns(1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(-1, -3, false)).Returns(1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(largeNumber, largeNumber, false)).Returns(1.0m).SetName("largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -largeNumber, false)).Returns(1.0m).SetName("-largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, largeNumber, false)).Returns(2.0m).SetName("2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, -largeNumber, false)).Returns(2.0m).SetName("-2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, 2 * largeNumber, false)).Returns(0.5m).SetName("largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, -2 * largeNumber, false)).Returns(0.5m).SetName("-largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, 2 * largeNumber, false)).Returns(1.5m).SetName("3 * largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, -2 * largeNumber, false)).Returns(1.5m).SetName("-3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, 3 * largeNumber, false)).Returns(2.0m / 3.0m).SetName("2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, -2 * largeNumber, false)).Returns(3.0m / 2.0m).SetName("-3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(Fraction.FromDecimal(decimal.MaxValue)).Returns(decimal.MaxValue).SetName("decimal.MaxValue");
            yield return new TestCaseData(Fraction.FromDecimal(decimal.MaxValue, false)).Returns(decimal.MaxValue).SetName("decimal.MaxValue(reduce = false)");
            yield return new TestCaseData(new Fraction(largeNumber, BigInteger.One, false)).Returns(decimal.MaxValue).SetName("largeNumber / 1");
            yield return new TestCaseData(new Fraction(largeNumber * largeNumber, largeNumber, false)).Returns(decimal.MaxValue).SetName("largeNumber^2 / largeNumber");
            yield return new TestCaseData(new Fraction(BigInteger.One, largeNumber, false)).Returns(0.0m).SetName("1 / largeNumber");
            yield return new TestCaseData(new Fraction(BigInteger.MinusOne, -largeNumber, false)).Returns(0.0m).SetName("-1 / -largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, largeNumber * largeNumber, false)).Returns(0.0m).SetName("largeNumber / largeNumber^2");
            yield return new TestCaseData(new Fraction(-largeNumber, -(largeNumber * largeNumber), false)).Returns(0.0m).SetName("-largeNumber / -largeNumber^2");
            yield return new TestCaseData(new Fraction(40, largeNumber, false)).Returns(20 / decimal.MaxValue).SetName("40 / (2 * decimal.MaxValue)");
            // Negative cases
            yield return new TestCaseData(new Fraction(-2)).Returns(-2.0m);
            yield return new TestCaseData(new Fraction(2, -1, false)).Returns(-2.0m);
            yield return new TestCaseData(new Fraction(-1, 2, false)).Returns(-0.5m);
            yield return new TestCaseData(new Fraction(1, -2, false)).Returns(-0.5m);
            yield return new TestCaseData(new Fraction(-1, 3, false)).Returns(-1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(1, -3, false)).Returns(-1.0m / 3.0m);
            yield return new TestCaseData(new Fraction(-largeNumber, largeNumber, false)).Returns(-1.0m).SetName("-largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -largeNumber, false)).Returns(-1.0m).SetName("largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, largeNumber, false)).Returns(-2.0m).SetName("-2 * largeNumber / largeNumber");
            yield return new TestCaseData(new Fraction(2 * largeNumber, -largeNumber, false)).Returns(-2.0m).SetName("2 * largeNumber / -largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, 2 * largeNumber, false)).Returns(-0.5m).SetName("-largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(largeNumber, -2 * largeNumber, false)).Returns(-0.5m).SetName("largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(-3 * largeNumber, 2 * largeNumber, false)).Returns(-1.5m).SetName("-3 * largeNumber / 2 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, -2 * largeNumber, false)).Returns(-1.5m).SetName("3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(new Fraction(-2 * largeNumber, 3 * largeNumber, false)).Returns(-2.0m / 3.0m).SetName("-2 * largeNumber / 3 * largeNumber");
            yield return new TestCaseData(new Fraction(3 * largeNumber, -2 * largeNumber, false)).Returns(-3.0m / 2.0m).SetName("3 * largeNumber / -2 * largeNumber");
            yield return new TestCaseData(Fraction.FromDecimal(decimal.MinValue)).Returns(decimal.MinValue).SetName("decimal.MaxValue");
            yield return new TestCaseData(Fraction.FromDecimal(decimal.MinValue, false)).Returns(decimal.MinValue).SetName("decimal.MaxValue(reduce = false)");
            yield return new TestCaseData(new Fraction(-largeNumber, BigInteger.One, false)).Returns(decimal.MinValue).SetName("-largeNumber / 1");
            yield return new TestCaseData(new Fraction(largeNumber * largeNumber, -largeNumber, false)).Returns(decimal.MinValue).SetName("largeNumber^2 / -largeNumber");
            yield return new TestCaseData(new Fraction(BigInteger.MinusOne, largeNumber, false)).Returns(0.0m).SetName("-1 / largeNumber");
            yield return new TestCaseData(new Fraction(BigInteger.One, -largeNumber, false)).Returns(0.0m).SetName("1 / -largeNumber");
            yield return new TestCaseData(new Fraction(-largeNumber, largeNumber * largeNumber, false)).Returns(0.0m).SetName("-largeNumber / largeNumber^2");
            yield return new TestCaseData(new Fraction(largeNumber, -(largeNumber * largeNumber), false)).Returns(0.0m).SetName("largeNumber / -largeNumber^2");
            yield return new TestCaseData(new Fraction(-40, largeNumber, false)).Returns(-20 / decimal.MaxValue).SetName("-40 / (2 * decimal.MaxValue)");
            yield return new TestCaseData(new Fraction(40, -largeNumber, false)).Returns(-20 / decimal.MaxValue).SetName("40 / -(2 * decimal.MaxValue)");
        }
    }
    
    [Test]
    [TestCaseSource(nameof(TestCases))]
    public decimal Result_should_match_expected_value(Fraction fraction) {
        return fraction.ToDecimalSaturating();
    }
}
