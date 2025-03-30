using BenchmarkDotNet.Attributes;

namespace Fractions.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class RoundingBenchmarks {
    
    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int NbDecimals { get; set; }
    
    [Params(MidpointRounding.AwayFromZero)]
    public MidpointRounding RoundingMode { get; set; }
    
    public static IEnumerable<Fraction> TestValues => [    
        // normal decimals
        Fraction.FromDecimal(2.5m, false),
        Fraction.FromDecimal(1.345m, false),
        Fraction.FromDecimal(123.456m, false),
        // small decimals
        new Fraction(1, 123456789, false),
        new Fraction(-1, 123456789, false),
        // high precision fractions
        Fraction.FromDoubleRounded(Math.PI, 16, false),
        Fraction.FromDoubleRounded(-Math.PI, 16, false),
        // non-terminating fractions
        new Fraction(8, 3, false),
        new Fraction(400, 3, false),
        new Fraction(2, 3, false),
        new Fraction(-2, 3, false),
    ];
    
    [Benchmark]
    [ArgumentsSource(nameof(TestValues))]
    public Fraction Round(Fraction fraction) {
        return Fraction.Round(fraction, NbDecimals, RoundingMode);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class RoundingWithoutReductionBenchmarks {

    [Params(1, 2, 4, 8, 16, 32, 64)]
    public int NbDecimals { get; set; }
    
    [Params(MidpointRounding.AwayFromZero)]
    public MidpointRounding RoundingMode { get; set; }
    
    public static IEnumerable<Fraction> TestValues => [    
        // normal decimals
        Fraction.FromDecimal(2.5m, false),
        Fraction.FromDecimal(1.345m, false),
        Fraction.FromDecimal(123.456m, false),
        // small decimals
        new Fraction(1, 123456789, false),
        new Fraction(-1, 123456789, false),
        // high precision fractions
        Fraction.FromDoubleRounded(Math.PI, 16, false),
        Fraction.FromDoubleRounded(-Math.PI, 16, false),
        // non-terminating fractions
        new Fraction(8, 3, false),
        new Fraction(400, 3, false),
        new Fraction(2, 3, false),
        new Fraction(-2, 3, false),
    ];
    
    [Benchmark]
    [ArgumentsSource(nameof(TestValues))]
    public Fraction Round(Fraction fraction) {
        return Fraction.Round(fraction, NbDecimals, RoundingMode, false);
    }
}
