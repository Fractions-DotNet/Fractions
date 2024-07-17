using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace Fractions.Benchmarks;

[MemoryDiagnoser]
public class ConvertToNumberBenchmarks {

    public static IEnumerable<Fraction> FractionsToConvert => [
        Fraction.Zero, 
        Fraction.One, 
        10,
        new Fraction(1, 10),
        new Fraction(1, 3),
        new Fraction(int.MaxValue),
        new Fraction(decimal.MaxValue),
        new Fraction( -4 * (BigInteger)decimal.MaxValue, 2 * (BigInteger)decimal.MaxValue, false),
        new Fraction( 3 * (BigInteger)decimal.MaxValue, 200 * (BigInteger)decimal.MaxValue, false),
        new Fraction( -4 * (BigInteger)double.MaxValue, 2 * (BigInteger)double.MaxValue, false),
        new Fraction( 3 * (BigInteger)double.MaxValue, 200 * (BigInteger)double.MaxValue, false)
    ];
    
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(FractionsToConvert))]
    public double ConvertToDouble(Fraction fraction) {
        return fraction.ToDouble();
    }

    [Benchmark]
    [ArgumentsSource(nameof(FractionsToConvert))]
    public decimal ConvertToDecimal(Fraction fraction) {
        return fraction.ToDecimal();
    }

    [Benchmark]
    [ArgumentsSource(nameof(FractionsToConvert))]
    public BigInteger ConvertToBigInteger(Fraction fraction) {
        return fraction.ToBigInteger();
    }
}
