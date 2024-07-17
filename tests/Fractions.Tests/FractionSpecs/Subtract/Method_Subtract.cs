using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.Subtract;

[TestFixture]
// German: Wenn 2 Fünftel mit 1 Fünftel subtrahiert wird
public class When_subtracting_2_fifths_by_1_fifth : Spec {
    private Fraction _a;
    private Fraction _b;
    private Fraction _result;

    public override void SetUp() {
        _a = new Fraction(2, 5);
        _b = new Fraction(1, 5);
    }

    public override void Act() {
        _result = _a.Subtract(_b);
    }

    [Test]
    // German: Soll der Zähler im Ergebnis 1 sein
    public void The_numerator_in_the_result_should_be_1() {
        _result.Numerator.Should().Be(1);
    }

    [Test]
    // German: Soll der Nenner im Ergebnis 5 sein
    public void The_denominator_in_the_result_should_be_5() {
        _result.Denominator.Should().Be(5);
    }
}

[TestFixture]
// German: Wenn 1 Fünftel mit 2 Fünftel subtrahiert wird
public class When_subtracting_1_fifth_by_2_fifths : Spec {
    private Fraction _a;
    private Fraction _b;
    private Fraction _result;

    public override void SetUp() {
        _a = new Fraction(1, 5);
        _b = new Fraction(2, 5);
    }

    public override void Act() {
        _result = _a.Subtract(_b);
    }

    [Test]
    // German: Soll der Zähler im Ergebnis minus 1 sein
    public void The_numerator_in_the_result_should_be_minus_1() {
        _result.Numerator.Should().Be(-1);
    }

    [Test]
    // German: Soll der Nenner im Ergebnis 5 sein
    public void The_denominator_in_the_result_should_be_5() {
        _result.Denominator.Should().Be(5);
    }
}

[TestFixture]
// German: Wenn 1 Fünftel mit 1 Fünftel subtrahiert wird
public class When_subtracting_1_fifth_by_1_fifth : Spec {
    private Fraction _a;
    private Fraction _b;
    private Fraction _result;

    public override void SetUp() {
        _a = new Fraction(1, 5);
        _b = new Fraction(1, 5);
    }

    public override void Act() {
        _result = _a.Subtract(_b);
    }

    [Test]
    // German: Soll der Zähler im Ergebnis 0 sein
    public void The_numerator_in_the_result_should_be_0() {
        _result.Numerator.Should().Be(0);
    }

    [Test]
    // German: Soll der Nenner im Ergebnis 1 sein
    public void The_denominator_in_the_result_should_be_1() {
        _result.Denominator.Should().Be(1);
    }

    [Test]
    // German: Soll der Wert des Bruches als Null erkannt werden
    public void The_value_of_the_fraction_should_be_recognized_as_zero() {
        _result.IsZero.Should().BeTrue();
    }
}

[TestFixture]
public class When_subtracting_with_NaN {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            // NaN with NaN
            yield return new TestCaseData(Fraction.NaN, Fraction.NaN, Fraction.NaN);

            // NaN with any number
            yield return new TestCaseData(Fraction.NaN, Fraction.PositiveInfinity, Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(5, 4), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(-5, -5, false), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, Fraction.One, Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(4, 5), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, Fraction.Zero, Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, Fraction.MinusOne, Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(-5, 4), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(-4, 5), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, new Fraction(5, -5, false), Fraction.NaN);
            yield return new TestCaseData(Fraction.NaN, Fraction.NegativeInfinity, Fraction.NaN);

            // Any number with NaN
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(new Fraction(5, 4), Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(Fraction.One, Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(new Fraction(4, 5), Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(Fraction.Zero, Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(Fraction.MinusOne, Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(new Fraction(-5, 4), Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(new Fraction(-4, 5), Fraction.NaN, Fraction.NaN);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.NaN, Fraction.NaN);
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void The_result_is_always_NaN(Fraction a, Fraction b, Fraction expected) {
        var result = a.Subtract(b);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
}

[TestFixture]
public class When_subtracting_with_infinity {
    private static IEnumerable<TestCaseData> TestCases {
        get {
            // positive infinity with positive infinity
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.PositiveInfinity, Fraction.NaN);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(5, 0, false), Fraction.NaN);

            // positive infinity with any other number
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(5, 4), Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.One, Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(4, 5), Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.Zero, Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.MinusOne, Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-5, 4), Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-4, 5), Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, Fraction.NegativeInfinity,
                Fraction.PositiveInfinity);
            yield return new TestCaseData(Fraction.PositiveInfinity, new Fraction(-5, 0, false),
                Fraction.PositiveInfinity);

            // negative infinity with negative infinity
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.NegativeInfinity, Fraction.NaN);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-5, 0, false), Fraction.NaN);

            // negative infinity with any other number
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(5, 4), Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.One, Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(4, 5), Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.Zero, Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.MinusOne, Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-5, 4), Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(-4, 5), Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, Fraction.PositiveInfinity,
                Fraction.NegativeInfinity);
            yield return new TestCaseData(Fraction.NegativeInfinity, new Fraction(5, 0, false),
                Fraction.NegativeInfinity);
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void The_result_is_always_NaN_or_Infinity(Fraction a, Fraction b, Fraction expected) {
        var result = a.Subtract(b);
        Assert.That(result, Is.EqualTo(expected).Using(StrictTestComparer.Instance));
    }
}

[TestFixture]
public class When_using_the_decrementing_operator : Spec {
    [Test]
    public void It_should_decrement_the_fraction_by_one() {
        var fraction = new Fraction(3, 2);
        fraction--;
        Assert.That(fraction, Is.EqualTo(new Fraction(1, 2)));
    }
}
