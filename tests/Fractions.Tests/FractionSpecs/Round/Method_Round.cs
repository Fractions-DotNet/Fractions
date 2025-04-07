using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.Round;

[TestFixture]
public class When_rounding_a_decimal_fraction : Spec {
    private static readonly Fraction[] DecimalFractions = [
        0, 1, -1, 10, -10,
        0.5m, -0.5m, 0.55m, -0.55m, 1.5m, -1.5m, 1.55m, -1.55m,
        0.1m, -0.1m, 0.15m, -0.15m, 1.2m, -1.2m, 1.25m, -1.25m,
        1.5545665434654m, -1.5545665434654m,
        15.545665434654m, -15.545665434654m,
        155.45665434654m, -155.45665434654m,
        1554.5665434654m, -1554.5665434654m,
        15545.665434654m, -15545.665434654m,
        155456.65434654m, -155456.65434654m,
        new Fraction(1, 3), new Fraction(-1, 3),
        new Fraction(2, 3), new Fraction(-2, 3),
        new Fraction(1999999999999999991, 3), new Fraction(-1999999999999999991, 3),
        new Fraction(2000000000000000001, 3), new Fraction(-2000000000000000001, 3),
        new Fraction(10, 20, false), new Fraction(-10, -20, false),
        new Fraction(-10, 20, false), new Fraction(10, -20, false),
        new Fraction(9, 20, false), new Fraction(-9, -20, false),
        new Fraction(-9, 20, false), new Fraction(9, -20, false),
        new Fraction(-45, -30, false), new Fraction(45, -30, false)
    ];

    private static readonly int[] DecimalPlaces = Enumerable.Range(0, 6).ToArray();

    private static readonly MidpointRounding[] RoundingModes = [
        MidpointRounding.ToEven,
        MidpointRounding.AwayFromZero,
#if NET
        MidpointRounding.ToZero,
        MidpointRounding.ToNegativeInfinity,
        MidpointRounding.ToPositiveInfinity
#endif
    ];

    private static IEnumerable RoundToBigIntegerTestCases =>
        from decimalFraction in DecimalFractions
        from midpointRounding in RoundingModes
        select new TestCaseData(decimalFraction, midpointRounding)
            .Returns((BigInteger)decimal.Round((decimal)decimalFraction, midpointRounding));


    [Test]
    [TestCaseSource(nameof(RoundToBigIntegerTestCases))]
    public BigInteger The_integral_result_should_be_correct_for_all_decimal_values(Fraction fraction,
        MidpointRounding roundingMode) {
        return Fraction.RoundToBigInteger(fraction, roundingMode);
    }

    [Test]
    [TestCaseSource(nameof(DecimalFractions))]
    public void The_integer_result_should_be_rounded_ToEven_by_default(Fraction fraction) {
        var expected = Fraction.RoundToBigInteger(fraction, MidpointRounding.ToEven);
        Assert.That(Fraction.RoundToBigInteger(fraction), Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }

    private static IEnumerable RoundToFractionTestCases =>
        from decimalFraction in DecimalFractions
        from decimalPlaces in DecimalPlaces
        from midpointRounding in RoundingModes
        select new TestCaseData(decimalFraction, decimalPlaces, midpointRounding,
            new Fraction(decimal.Round((decimal)decimalFraction, decimalPlaces, midpointRounding)));

    [Test]
    [TestCaseSource(nameof(RoundToFractionTestCases))]
    public void The_fractional_result_should_be_correct_for_all_decimal_values(Fraction fraction, int decimalPlaces,
        MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
    
    [Test]
    [TestCaseSource(nameof(RoundToFractionTestCases))]
    public void The_fractional_result_should_be_correct_for_all_decimal_values_without_normalization(Fraction fraction, int decimalPlaces,
        MidpointRounding roundingMode, Fraction expected) {
        Fraction.Round(fraction, decimalPlaces, roundingMode, false).Should().Be(expected);
    }

    [Test]
    public void The_result_of_rounding_with_negative_accuracy_should_be_an_ArgumentOutOfRangeException() {
        Invoking(() => Fraction.Round(Fraction.One, -1)).Should().Throw<ArgumentOutOfRangeException>();
    }

    private static IEnumerable RoundRepeatingFractionTestCases {
        get {
            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // one third: (1/3) rounded to 2 decimals should always be 0.33 (33/100)
                yield return new TestCaseData(new Fraction(1, 3), 2, roundingMode, new Fraction(33, 100)); // 0.33
                // minus one third: (-1/3) rounded to 2 decimals should always be -0.33 (-33/100)
                yield return new TestCaseData(new Fraction(-1, 3), 2, roundingMode, new Fraction(-33, 100)); // -0.33
            }

#if NET
            foreach (var roundingMode in new[] { MidpointRounding.ToNegativeInfinity, MidpointRounding.ToZero }) {
                // one third: (1/3) rounded to 2 decimals should always be 0.33 (33/100)
                yield return new TestCaseData(new Fraction(1, 3), 2, roundingMode, new Fraction(33, 100)); // 0.33
            }

            // one third: (1/3) rounded to 2 decimals should always be 0.34 (34/100) when rounding up
            yield return new TestCaseData(new Fraction(1, 3), 2, MidpointRounding.ToPositiveInfinity,
                new Fraction(34, 100)); // 0.34

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus one third: (-1/3) rounded to 2 decimals should always be -0.33 (-33/100)
                yield return new TestCaseData(new Fraction(-1, 3), 2, roundingMode, new Fraction(-33, 100)); // -0.33
            }

            // minus one third: (-1/3) rounded to 2 decimals should always be -0.34 (-34/100) when rounding down
            yield return new TestCaseData(new Fraction(-1, 3), 2, MidpointRounding.ToNegativeInfinity,
                new Fraction(-34, 100)); // -0.34
#endif
            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // two thirds: (2/3) rounded to 2 decimals should always be 0.67 (67/100)
                yield return new TestCaseData(new Fraction(2, 3), 2, roundingMode, new Fraction(67, 100)); // 0.67
                // minus one third: (-2/3) rounded to 2 decimals should always be -0.67 (-67/100)
                yield return new TestCaseData(new Fraction(-2, 3), 2, roundingMode, new Fraction(-67, 100)); // -0.67
            }
#if NET
            // two thirds: (2/3) rounded to 2 decimals should always be 0.67 (67/100)
            yield return new TestCaseData(new Fraction(2, 3), 2, MidpointRounding.ToPositiveInfinity,
                new Fraction(67, 100)); // 0.67

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToNegativeInfinity }) {
                // two thirds: (2/3) rounded to 2 decimals should always be 0.66 (66/100) when rounded down
                yield return new TestCaseData(new Fraction(2, 3), 2, roundingMode, new Fraction(66, 100)); // 0.66
            }

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus two thirds: (-2/3) rounded to 2 decimals should always be -0.66 (-66/100)
                yield return new TestCaseData(new Fraction(-2, 3), 2, roundingMode, new Fraction(-66, 100)); // 0.66
            }

            // minus two thirds: (-2/3) rounded to 2 decimals should always be -0.67 (-67/100) when rounded down
            yield return new TestCaseData(new Fraction(-2, 3), 2, MidpointRounding.ToNegativeInfinity,
                new Fraction(-67, 100)); // 0.67
#endif

            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // four thirds: (4/3) rounded to 2 decimals should always be 1.33 (133/100)
                yield return new TestCaseData(new Fraction(4, 3), 2, roundingMode, new Fraction(133, 100)); // 1.33
                // minus four thirds (-4/3) rounded to 2 decimals should always be -1.33 (-133/100)
                yield return new TestCaseData(new Fraction(-4, 3), 2, roundingMode, new Fraction(-133, 100)); // -1.33
            }

#if NET
            foreach (var roundingMode in new[] { MidpointRounding.ToNegativeInfinity, MidpointRounding.ToZero }) {
                // four thirds: (4/3) rounded to 2 decimals should always be 1.33 (133/100)
                yield return new TestCaseData(new Fraction(4, 3), 2, roundingMode, new Fraction(133, 100)); // 1.33
            }

            // four thirds: (4/3) rounded to 2 decimals should always be 1.34 (134/100) when rounding up
            yield return new TestCaseData(new Fraction(4, 3), 2, MidpointRounding.ToPositiveInfinity,
                new Fraction(134, 100)); // 1.34

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus four thirds: (-4/3) rounded to 2 decimals should always be -1.33 (-133/100)
                yield return new TestCaseData(new Fraction(-4, 3), 2, roundingMode, new Fraction(-133, 100)); // -1.33
            }

            // minus four thirds: (-4/3) rounded to 2 decimals should always be -1.34 (-134/100) when rounding down
            yield return new TestCaseData(new Fraction(-4, 3), 2, MidpointRounding.ToNegativeInfinity,
                new Fraction(-134, 100)); // -1.34
#endif
        }
    }


    [Test]
    [TestCaseSource(nameof(RoundRepeatingFractionTestCases))]
    public void The_fractional_result_should_be_correct_for_all_repeating_fractions(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }

    [Test]
    [TestCaseSource(nameof(RoundRepeatingFractionTestCases))]
    public void The_fractional_result_should_be_correct_for_all_repeating_fractions_without_normalization(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        Fraction.Round(fraction, decimalPlaces, roundingMode).Should().Be(expected);
    }

    [Test]
    public void The_fractional_result_should_be_correct_for_very_large_values() {
        var a = new BigInteger(123456789987654321);
        var b = new BigInteger(123456789987654321) * BigInteger.Pow(10, 18);
        var largeValue = a + b; // should represent the value 123456789987654321123456789987654321
        var middle = new Fraction(largeValue, BigInteger.Pow(10, 18)); // place the decimal point in the middle
        var valueToTest = middle / BigInteger.Pow(10, 17);

        var roundedValue = Fraction.Round(valueToTest, 36);

        roundedValue.Should().Be(valueToTest);
    }

    [Test]
    public void The_fractional_result_should_be_correct_for_very_large_values_without_normalization() {
        var a = new BigInteger(123456789987654321);
        var b = new BigInteger(123456789987654321) * BigInteger.Pow(10, 18);
        var largeValue = a + b; // should represent the value 123456789987654321123456789987654321
        var middle = new Fraction(largeValue, BigInteger.Pow(10, 18)); // place the decimal point in the middle
        var valueToTest = middle / BigInteger.Pow(10, 17);

        var roundedValue = Fraction.Round(valueToTest, 36, MidpointRounding.ToEven, false);

        roundedValue.Should().Be(valueToTest);
    }

    private static IEnumerable RoundNaNToBigIntegerTestCases =>
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.NaN, midpointRounding);

    [Test]
    [TestCaseSource(nameof(RoundNaNToBigIntegerTestCases))]
    public void The_result_of_rounding_NaN_to_BigInteger_should_be_a_OverflowException(Fraction fraction,
        MidpointRounding roundingMode) {
        Invoking(() => Fraction.RoundToBigInteger(fraction, roundingMode)).Should().Throw<OverflowException>();
    }

    private static IEnumerable RoundPositiveInfinityToBigIntegerTestCases =>
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.PositiveInfinity, midpointRounding);

    [Test]
    [TestCaseSource(nameof(RoundPositiveInfinityToBigIntegerTestCases))]
    public void The_result_of_rounding_PositiveInfinity_to_BigInteger_should_throw_a_OverflowException(
        Fraction fraction, MidpointRounding roundingMode) {
        Invoking(() => Fraction.RoundToBigInteger(fraction, roundingMode)).Should().Throw<OverflowException>();
    }

    private static IEnumerable RoundNegativeInfinityToBigIntegerTestCases =>
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.NegativeInfinity, midpointRounding);

    [Test]
    [TestCaseSource(nameof(RoundNegativeInfinityToBigIntegerTestCases))]
    public void The_result_of_rounding_NegativeInfinity_to_BigInteger_should_throw_a_OverflowException(
        Fraction fraction, MidpointRounding roundingMode) {
        Invoking(() => Fraction.RoundToBigInteger(fraction, roundingMode)).Should().Throw<OverflowException>();
    }

    [Test]
    public void The_result_of_rounding_with_invalid_MidpointRounding_should_be_an_ArgumentOutOfRangeException() {
        Invoking(() => Fraction.RoundToBigInteger(new Fraction(1, 2), (MidpointRounding)(-1))).Should().Throw<ArgumentOutOfRangeException>();
    }

    private static IEnumerable RoundNaNTestCases =>
        from decimalPlaces in DecimalPlaces
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.NaN, decimalPlaces, midpointRounding, Fraction.NaN);

    [Test]
    [TestCaseSource(nameof(RoundNaNTestCases))]
    public void The_result_of_rounding_NaN_should_be_NaN(Fraction fraction, int decimalPlaces,
        MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
    
    [Test]
    [TestCaseSource(nameof(RoundNaNTestCases))]
    public void The_result_of_rounding_NaN_should_be_NaN_without_normalization(Fraction fraction, int decimalPlaces,
        MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode, false);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }

    private static IEnumerable RoundPositiveInfinityTestCases =>
        from decimalPlaces in DecimalPlaces
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.PositiveInfinity, decimalPlaces, midpointRounding, Fraction.PositiveInfinity);

    [Test]
    [TestCaseSource(nameof(RoundPositiveInfinityTestCases))]
    public void The_result_of_rounding_PositiveInfinity_should_be_PositiveInfinity(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
    
    [Test]
    [TestCaseSource(nameof(RoundPositiveInfinityTestCases))]
    public void The_result_of_rounding_PositiveInfinity_should_be_PositiveInfinity_without_normalization(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode, false);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }

    private static IEnumerable RoundNegativeInfinityTestCases =>
        from decimalPlaces in DecimalPlaces
        from midpointRounding in RoundingModes
        select new TestCaseData(Fraction.NegativeInfinity, decimalPlaces, midpointRounding, Fraction.NegativeInfinity);

    [Test]
    [TestCaseSource(nameof(RoundNegativeInfinityTestCases))]
    public void The_result_of_rounding_NegativeInfinity_should_be_NegativeInfinity(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
    
    [Test]
    [TestCaseSource(nameof(RoundNegativeInfinityTestCases))]
    public void The_result_of_rounding_NegativeInfinity_should_be_NegativeInfinity_without_normalization(Fraction fraction,
        int decimalPlaces, MidpointRounding roundingMode, Fraction expected) {
        var result = Fraction.Round(fraction, decimalPlaces, roundingMode, false);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
}
