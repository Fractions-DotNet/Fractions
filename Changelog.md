# Changelog

## 9.0.0

- PR [Implemented the INumber<Fraction> interface](https://github.com/Fractions-DotNet/Fractions/pull/96) by [lipchev](https://github.com/lipchev)
- PR [More DecimalQuantityFormatter performance optimizations](https://github.com/Fractions-DotNet/Fractions/pull/115) by [lipchev](https://github.com/lipchev)

### Breaking changes

- When using the .NET8 assembly, the data type implements the two interfaces `INumber<Fraction>` and `ISignedNumber<Fraction>` with the corresponding support but also possible side effects when using the generic [math functions](https://learn.microsoft.com/en-us/dotnet/standard/generics/math). For more information, see this [Microsoft blog article](https://devblogs.microsoft.com/dotnet/dotnet-7-generic-math/).

## 8.3.2
- PR [Optimizing the PowerOfTen function for exponents in the range 18-54](https://github.com/danm-de/Fractions/pull/110) by [lipchev](https://github.com/lipchev)
- PR [FromDecimal: replaced the usage of BigInteger.Pow(TEN, exponent)](https://github.com/danm-de/Fractions/pull/109) by [lipchev](https://github.com/lipchev)
- PR [FromDoubleRounded: replaced the usages of BigInteger.Pow(TEN, exponent)](https://github.com/danm-de/Fractions/pull/108) by [lipchev](https://github.com/lipchev)
- PR [FromDecimal: replaced the usage of BigInteger.Pow(TEN, exponent)](https://github.com/danm-de/Fractions/pull/109) by [lipchev](https://github.com/lipchev)

## 8.3.1
- Fixed [#105](https://github.com/danm-de/Fractions/issues/105): ArgumentOutOfRangeException thrown from the TryParse method by [lipchev](https://github.com/lipchev)
- Fixed [#102](https://github.com/danm-de/Fractions/issues/102): DecimalNotationFormatter with "G2" returns "2.0" by [lipchev](https://github.com/lipchev)
- Fixed [#107](https://github.com/danm-de/Fractions/issues/107): The comparison operators with NaN should return false

## 8.3.0

- Re-implement the Sqrt function (fixes [#97](https://github.com/danm-de/Fractions/issues/97)) by [lipchev](https://github.com/lipchev)

## 8.2.0

- Fixed [#100](https://github.com/danm-de/Fractions/issues/100): Fractions 8.1.1 depends on Newtonsoft.Json (>= 13.0.3). Reported by [lipchev](https://github.com/lipchev)
- Pinned FluentAssertion to version 7.x after the [commercial rug pull](https://youtu.be/ZFc6jcaM6Ms)
- Adding a Fraction.Round overload with the "normalize" parameter [#95](https://github.com/danm-de/Fractions/pull/95) by [lipchev](https://github.com/lipchev)

## 8.1.1

- Fixes [#93](https://github.com/danm-de/Fractions/issues/93): DecimalNotationFormatter throws FormatException for some custom formats (via [Pull-Request](https://github.com/danm-de/Fractions/pulls)) by [lipchev](https://github.com/lipchev)
- Fixes [#92](https://github.com/danm-de/Fractions/issues/92): Mark the MathExt exensions as obsolete (reported by [lipchev](https://github.com/lipchev))
- Fixes Fraction.Sqrt(): When the root was calculated from a fraction with a very large numerator and denominator the result could have exceeded double's number range and returned NaN.

## 8.1.0

- Improving the performance of the Sqrt extension method [#88](https://github.com/danm-de/Fractions/pull/88) by [lipchev](https://github.com/lipchev)
- DecimalNotationFormatter: extracted a non-boxing Format overload [89](https://github.com/danm-de/Fractions/pull/89) by [lipchev](https://github.com/lipchev)
- Added two JSON serializes: StringFractionJsonConverter and StructuralFractionJsonConverter
- Fixes when TryParse was called with a CultureInfo that has a single symbol for infinity/NaN.

## 8.0.4

- Code / Build cleanups by [Marko Lahma](https://github.com/lahma):
  - PackageLicenseExpression will show up correctly on nuget.org and tools can analyze it
  - moving common properties to shared Directory.Build.props for easier maintenance
  - Add ContinuousIntegrationBuild to build setup
- Fixes [#85](https://github.com/danm-de/Fractions/pull/85) (by [Marko Lahma](https://github.com/lahma))

## 8.0.3

- Bugfix in `Fraction.FromDoubleRounded` [#83](https://github.com/danm-de/Fractions/issues/83)

## 8.0.2

- Bugfix in `Fraction.CompareTo` [#78](https://github.com/danm-de/Fractions/issues/78) by [lipchev](https://github.com/lipchev)
- Performance optimization in `Fraction.FromDoubleRounded` [#80](https://github.com/danm-de/Fractions/pull/80) by [lipchev](https://github.com/lipchev)
  Changed behavior: `ArgumentOutOfRangeException` is thrown when argument `significantDigits` is &lt; 1 or &gt; 17.

## 8.0.1

- Fixed nullable issue in Fraction.TryParse(..) reported in [#76](https://github.com/danm-de/Fractions/issues/76)
  Error (active) CS8604 Possible null reference argument for parameter 'value' in 'bool Fraction.TryParse(..)

## 8.0.0

- Added support for NaN and Infinity by [lipchev](https://github.com/lipchev)
- New properties IsNaN, IsInfinity, IsPositiveInfinity, IsNegativeInfinity by [lipchev](https://github.com/lipchev)
- Adding a debugger display proxy by [lipchev](https://github.com/lipchev)
- Various methods were optimized by [lipchev](https://github.com/lipchev)
- Use the potential of Span&lt;T&gt; where sensible and possible (TryParse, FromDecimal) by [lipchev](https://github.com/lipchev)
- No longer reduce non-normalized fractions when used in mathematical operations - by [lipchev](https://github.com/lipchev)

### Breaking changes

- Mathematical operations no longer automatically generate normalized fractions if one of the operands is an non-normalized fraction. This has an impact on your calculations, especially if you have used the `JsonFractionConverter` with default settings. In such cases, deserialized fractions create non-normalized fractions, which can lead to changed behavior when calling `ToString`. Please refer the README section [Working with non-normalized fractions](Readme.md#working-with-non-normalized-fractions)
- The default behavior of the `.Equals(Fraction)` method has changed: `Equals` now compares the calculated value from the numerator/denominator (1/2 = 2/4).
- The standard function `ToString()` now depends on the active culture (`CultureInfo.CurrentCulture`). The reason is that NaN and Infinity should be displayed in the system language or the corresponding symbol should be used.
- Argument name for `Fraction.TryParse(..)` changed from `fractionString` to `value`.
- A fraction of 0/0 no longer has the value 0, but means NaN (not a number). Any fraction in the form x/0 is no longer a valid number. A denominator with the value 0 corresponds to, depending on the numerator, NaN, PositiveInfinity or NegativeInfinity.

## 7.7.1

- Added hotfix FromString supporting all NumberStyles https://github.com/lipchev

## 7.7.0

- Added DecimalNotationFormatter + documentation by [lipchev](https://github.com/lipchev)
- Added Benchmark results summary (with Readme) by [lipchev](https://github.com/lipchev)
- Introduced the negation (-) operator by [lipchev](https://github.com/lipchev)
- Added extended xml comments to the FromDouble* methods by [lipchev](https://github.com/lipchev)
- Replace .Invert() with .Negate() method

## 7.6.1

- Incorrect result when rounding 1/3 with MidpointRounding.ToEven (fixes #39) by [lipchev](https://github.com/lipchev)

## 7.6.0

- Added method overload for FromDouble with significant digits by [lipchev](https://github.com/lipchev)
- Performance optimization of the FromDouble method by [lipchev](https://github.com/lipchev)

## 7.5.0

- Added benchmarks for the most common operations by [lipchev](https://github.com/lipchev)
- Added Fraction.Round/RoundToBigInteger functions by [lipchev](https://github.com/lipchev)
- Make the operator from decimal implicit by [lipchev](https://github.com/lipchev)
- Support for parsing long/very precise numbers by [lipchev](https://github.com/lipchev)
- Added support for `ReadOnlySpan<char>` when using TryParse(..)
- Added target framework .NET8.0
- Updated documentation
- Many thanks to @lipchev for the contributions

## 7.4.1

- Code cleanup (use new language features)
- fix https://github.com/danm-de/Fractions/issues/27 reported by [lipchev](https://github.com/lipchev)

## 7.4.0

- The CLSCompliant(true) attribute was added by [lipchev](https://github.com/lipchev)
- fix invalid characters in test names by [lipchev](https://github.com/lipchev)
- Updated nuget packages

## 7.3.0

- New Reciprocal() method contributed by https://github.com/teemka
- Updated nuget packages

## 7.2.1

- Updated Newtonsoft.Json to version 13.0.2 (Dependabot)

## 7.2.0

- Removed System.Runtime.Numerics dependency. Thanks to @stan-sz https://github.com/danm-de/Fractions/pull/18

## 7.1.0

- New Sqrt() extension method contributed by https://github.com/MadsKirkFoged

## 7.0.0

- Removed unsupported framework DLL netstandard1.1 (Issue [#9](https://github.com/danm-de/Fractions/issues/9))
- Removed deplicated and unecessary framework DLLs net45, net48, net5.0 and netcoreapp3.1 (Issue [#9](https://github.com/danm-de/Fractions/issues/9))

## 6.0.0

- Breaking change: `new Fraction(0, 0).ToString()` or `new Fraction(0, 3).ToString()` returns "0" instead of "0/0"
- Bugfix: `Fraction.Zero.ToString("m")` does not longer throw a divide by zero exception (Issue [#6](https://github.com/danm-de/Fractions/issues/6))
- Added new framework DLLs for net48, netstandard2.1, netcoreapp3.1 and net5.0