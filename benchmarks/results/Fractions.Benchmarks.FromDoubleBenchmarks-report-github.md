```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5608/22H2/2022Update)
AMD Ryzen 9 7900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 9.0.201
  [Host]   : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                                     | ReduceTerms | value                | Mean       | Error       | StdDev     | Gen0   | Allocated |
|------------------------------------------- |------------ |--------------------- |-----------:|------------:|-----------:|-------:|----------:|
| **Construct_FromDouble**                       | **False**       | **NaN**                  |   **6.728 ns** |   **0.2304 ns** |  **0.0126 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | False       | NaN                  |   3.521 ns |   0.5684 ns |  0.0312 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | NaN                  |   3.769 ns |   0.1392 ns |  0.0076 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | NaN                  |   3.760 ns |   0.2466 ns |  0.0135 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **-Infinity**            |   **6.818 ns** |   **0.7420 ns** |  **0.0407 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | False       | -Infinity            |   3.393 ns |   0.0276 ns |  0.0015 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | -Infinity            |   3.950 ns |   0.4628 ns |  0.0254 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | -Infinity            |   3.965 ns |   0.5442 ns |  0.0298 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **-7.92(...)4E+28 [22]** |  **73.560 ns** |   **2.9730 ns** |  **0.1630 ns** | **0.0091** |     **152 B** |
| Construct_FromDoubleRounded                | False       | -7.92(...)4E+28 [22] |   9.208 ns |   0.2743 ns |  0.0150 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | -7.92(...)4E+28 [22] | 122.619 ns |   6.8601 ns |  0.3760 ns | 0.0048 |      80 B |
| Construct_FromDoubleRoundedToFifteenDigits | False       | -7.92(...)4E+28 [22] |  59.533 ns |   2.5401 ns |  0.1392 ns | 0.0043 |      72 B |
| **Construct_FromDouble**                       | **False**       | **-0.02702702702702703** |  **59.621 ns** |   **3.6428 ns** |  **0.1997 ns** | **0.0076** |     **128 B** |
| Construct_FromDoubleRounded                | False       | -0.02702702702702703 |   8.729 ns |   0.9574 ns |  0.0525 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | -0.02702702702702703 |  31.680 ns |   0.4914 ns |  0.0269 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | -0.02702702702702703 |  37.496 ns |   1.5376 ns |  0.0843 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **False**       | **-3.69(...)7E-06 [23]** |  **60.210 ns** |  **12.2115 ns** |  **0.6694 ns** | **0.0081** |     **136 B** |
| Construct_FromDoubleRounded                | False       | -3.69(...)7E-06 [23] |  53.590 ns |   2.0874 ns |  0.1144 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | -3.69(...)7E-06 [23] |  38.760 ns |   4.6424 ns |  0.2545 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | -3.69(...)7E-06 [23] |  50.051 ns |   0.7785 ns |  0.0427 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **0**                    |   **6.744 ns** |   **0.1161 ns** |  **0.0064 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | False       | 0                    |   3.585 ns |   0.6016 ns |  0.0330 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 0                    |   3.558 ns |   0.2262 ns |  0.0124 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 0                    |   3.563 ns |   0.2190 ns |  0.0120 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **0.022046226218487758** |  **47.315 ns** |   **0.8687 ns** |  **0.0476 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 0.022046226218487758 | 172.996 ns |   3.0639 ns |  0.1679 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 0.022046226218487758 |  32.079 ns |   0.2331 ns |  0.0128 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 0.022046226218487758 |  36.556 ns |   4.5142 ns |  0.2474 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **False**       | **0.09999999999999999**  |  **46.930 ns** |   **0.3629 ns** |  **0.0199 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 0.09999999999999999  |   8.784 ns |   0.1796 ns |  0.0098 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 0.09999999999999999  |  40.392 ns |   0.3356 ns |  0.0184 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 0.09999999999999999  |  49.930 ns |   1.6502 ns |  0.0905 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **0.3333333333333333**   |  **47.259 ns** |   **2.3477 ns** |  **0.1287 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 0.3333333333333333   |   8.778 ns |   0.2287 ns |  0.0125 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 0.3333333333333333   |  32.410 ns |   0.2091 ns |  0.0115 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 0.3333333333333333   |  36.480 ns |   0.7786 ns |  0.0427 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **False**       | **1.345**                |  **32.292 ns** |   **2.1974 ns** |  **0.1204 ns** | **0.0038** |      **64 B** |
| Construct_FromDoubleRounded                | False       | 1.345                |  43.257 ns |   1.4175 ns |  0.0777 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 1.345                |  42.868 ns |   1.3338 ns |  0.0731 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 1.345                |  50.842 ns |   1.2132 ns |  0.0665 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **3.141592653589793**    |  **48.718 ns** |   **2.2856 ns** |  **0.1253 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 3.141592653589793    | 117.126 ns |   4.1611 ns |  0.2281 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 3.141592653589793    |  38.535 ns |   0.9588 ns |  0.0526 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 3.141592653589793    |  59.507 ns |   1.2222 ns |  0.0670 ns | 0.0057 |      96 B |
| **Construct_FromDouble**                       | **False**       | **42**                   |  **50.780 ns** |   **1.1968 ns** |  **0.0656 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 42                   |   6.105 ns |   0.1102 ns |  0.0060 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | 42                   |  31.119 ns |   3.9544 ns |  0.2168 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 42                   |  30.349 ns |   3.9077 ns |  0.2142 ns |      - |         - |
| **Construct_FromDouble**                       | **False**       | **2110.11170524**        |  **52.685 ns** |   **1.8315 ns** |  **0.1004 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | False       | 2110.11170524        | 134.717 ns |   5.4662 ns |  0.2996 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | 2110.11170524        |  38.703 ns |   6.1943 ns |  0.3395 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 2110.11170524        |  53.614 ns |   1.8514 ns |  0.1015 ns | 0.0038 |      64 B |
| **Construct_FromDouble**                       | **False**       | **1024000000000**        |  **61.438 ns** |   **5.5104 ns** |  **0.3020 ns** | **0.0081** |     **136 B** |
| Construct_FromDoubleRounded                | False       | 1024000000000        |   7.815 ns |   0.3301 ns |  0.0181 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | 1024000000000        |  36.376 ns |   1.9408 ns |  0.1064 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 1024000000000        |  42.667 ns |   1.7401 ns |  0.0954 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **False**       | **5.9722E+24**           |  **67.674 ns** |   **4.0340 ns** |  **0.2211 ns** | **0.0091** |     **152 B** |
| Construct_FromDoubleRounded                | False       | 5.9722E+24           |   9.493 ns |   0.4490 ns |  0.0246 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | 5.9722E+24           |  44.739 ns |   1.2313 ns |  0.0675 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 5.9722E+24           |  45.679 ns |   1.2604 ns |  0.0691 ns | 0.0043 |      72 B |
| **Construct_FromDouble**                       | **False**       | **1.797(...)E+308 [23]** | **108.424 ns** |   **3.5303 ns** |  **0.1935 ns** | **0.0224** |     **376 B** |
| Construct_FromDoubleRounded                | False       | 1.797(...)E+308 [23] |  10.012 ns |   1.4455 ns |  0.0792 ns | 0.0091 |     152 B |
| Construct_FromDoubleRoundedToEightDigits   | False       | 1.797(...)E+308 [23] | 655.706 ns | 269.0861 ns | 14.7495 ns | 0.0181 |     304 B |
| Construct_FromDoubleRoundedToFifteenDigits | False       | 1.797(...)E+308 [23] | 652.190 ns | 459.5968 ns | 25.1921 ns | 0.0200 |     336 B |
| **Construct_FromDouble**                       | **False**       | **Infinity**             |   **6.814 ns** |   **0.0737 ns** |  **0.0040 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | False       | Infinity             |   3.183 ns |   0.0953 ns |  0.0052 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | False       | Infinity             |   3.926 ns |   0.0339 ns |  0.0019 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | False       | Infinity             |   3.937 ns |   0.1055 ns |  0.0058 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **NaN**                  |   **6.732 ns** |   **0.1335 ns** |  **0.0073 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | True        | NaN                  |   3.475 ns |   0.1771 ns |  0.0097 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | NaN                  |   3.736 ns |   0.0129 ns |  0.0007 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | NaN                  |   3.738 ns |   0.1176 ns |  0.0064 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **-Infinity**            |   **6.807 ns** |   **0.1116 ns** |  **0.0061 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | True        | -Infinity            |   3.373 ns |   0.2213 ns |  0.0121 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | -Infinity            |   3.949 ns |   0.0568 ns |  0.0031 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | -Infinity            |   3.932 ns |   0.2406 ns |  0.0132 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **-7.92(...)4E+28 [22]** |  **99.445 ns** |  **23.0377 ns** |  **1.2628 ns** | **0.0081** |     **136 B** |
| Construct_FromDoubleRounded                | True        | -7.92(...)4E+28 [22] |   9.672 ns |   1.0018 ns |  0.0549 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | -7.92(...)4E+28 [22] | 121.062 ns |  26.3900 ns |  1.4465 ns | 0.0048 |      80 B |
| Construct_FromDoubleRoundedToFifteenDigits | True        | -7.92(...)4E+28 [22] |  57.511 ns |  12.8223 ns |  0.7028 ns | 0.0043 |      72 B |
| **Construct_FromDouble**                       | **True**        | **-0.02702702702702703** | **116.695 ns** |   **8.9242 ns** |  **0.4892 ns** | **0.0114** |     **192 B** |
| Construct_FromDoubleRounded                | True        | -0.02702702702702703 |  10.692 ns |   0.2083 ns |  0.0114 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | -0.02702702702702703 |  39.951 ns |   0.8828 ns |  0.0484 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | -0.02702702702702703 |  56.035 ns |   1.2691 ns |  0.0696 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **True**        | **-3.69(...)7E-06 [23]** | **135.631 ns** |   **2.9876 ns** |  **0.1638 ns** | **0.0081** |     **136 B** |
| Construct_FromDoubleRounded                | True        | -3.69(...)7E-06 [23] |  60.769 ns |   0.7721 ns |  0.0423 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | -3.69(...)7E-06 [23] |  47.648 ns |   1.5720 ns |  0.0862 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | -3.69(...)7E-06 [23] |  56.724 ns |   0.7356 ns |  0.0403 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **0**                    |   **6.768 ns** |   **0.2159 ns** |  **0.0118 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | True        | 0                    |   3.564 ns |   0.0847 ns |  0.0046 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 0                    |   3.590 ns |   0.8458 ns |  0.0464 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 0                    |   3.570 ns |   0.0331 ns |  0.0018 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **0.022046226218487758** | **197.944 ns** |  **18.7701 ns** |  **1.0289 ns** | **0.0095** |     **160 B** |
| Construct_FromDoubleRounded                | True        | 0.022046226218487758 | 189.382 ns |   9.8778 ns |  0.5414 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 0.022046226218487758 |  94.890 ns |   2.5705 ns |  0.1409 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 0.022046226218487758 | 139.931 ns |  11.8011 ns |  0.6469 ns | 0.0057 |      96 B |
| **Construct_FromDouble**                       | **True**        | **0.09999999999999999**  |  **79.018 ns** |   **6.6254 ns** |  **0.3632 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | True        | 0.09999999999999999  |  10.349 ns |   1.6600 ns |  0.0910 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 0.09999999999999999  |  46.436 ns |   1.4999 ns |  0.0822 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 0.09999999999999999  |  54.318 ns |   1.4339 ns |  0.0786 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **0.3333333333333333**   |  **70.158 ns** |   **5.3248 ns** |  **0.2919 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | True        | 0.3333333333333333   |  10.447 ns |   1.2439 ns |  0.0682 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 0.3333333333333333   |  38.563 ns |   2.7164 ns |  0.1489 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 0.3333333333333333   |  55.099 ns |   6.8111 ns |  0.3733 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **True**        | **1.345**                |  **99.333 ns** |  **13.1079 ns** |  **0.7185 ns** | **0.0038** |      **64 B** |
| Construct_FromDoubleRounded                | True        | 1.345                |  54.613 ns |   2.1318 ns |  0.1169 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 1.345                |  78.648 ns |   3.6642 ns |  0.2008 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 1.345                |  87.107 ns |  16.9738 ns |  0.9304 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **3.141592653589793**    | **253.955 ns** |  **55.4950 ns** |  **3.0419 ns** | **0.0134** |     **224 B** |
| Construct_FromDoubleRounded                | True        | 3.141592653589793    | 130.474 ns |   6.7436 ns |  0.3696 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 3.141592653589793    |  54.644 ns |   3.3811 ns |  0.1853 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 3.141592653589793    | 124.220 ns |  25.0219 ns |  1.3715 ns | 0.0057 |      96 B |
| **Construct_FromDouble**                       | **True**        | **42**                   | **134.984 ns** |   **0.9464 ns** |  **0.0519 ns** | **0.0057** |      **96 B** |
| Construct_FromDoubleRounded                | True        | 42                   |   6.628 ns |   0.4532 ns |  0.0248 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | 42                   |  32.722 ns |   5.8085 ns |  0.3184 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 42                   |  32.075 ns |   0.8840 ns |  0.0485 ns |      - |         - |
| **Construct_FromDouble**                       | **True**        | **2110.11170524**        | **175.461 ns** |  **22.6926 ns** |  **1.2439 ns** | **0.0095** |     **160 B** |
| Construct_FromDoubleRounded                | True        | 2110.11170524        | 145.097 ns |  34.5077 ns |  1.8915 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | 2110.11170524        |  44.746 ns |   2.6546 ns |  0.1455 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 2110.11170524        | 118.650 ns |  17.6438 ns |  0.9671 ns | 0.0057 |      96 B |
| **Construct_FromDouble**                       | **True**        | **1024000000000**        | **184.948 ns** |  **18.2822 ns** |  **1.0021 ns** | **0.0114** |     **192 B** |
| Construct_FromDoubleRounded                | True        | 1024000000000        |   8.627 ns |   1.2931 ns |  0.0709 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | 1024000000000        |  36.524 ns |   4.8249 ns |  0.2645 ns | 0.0019 |      32 B |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 1024000000000        |  39.949 ns |  29.9521 ns |  1.6418 ns | 0.0019 |      32 B |
| **Construct_FromDouble**                       | **True**        | **5.9722E+24**           | **221.994 ns** |   **5.9083 ns** |  **0.3239 ns** | **0.0134** |     **224 B** |
| Construct_FromDoubleRounded                | True        | 5.9722E+24           |   9.751 ns |   0.3754 ns |  0.0206 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | 5.9722E+24           |  46.096 ns |   7.2076 ns |  0.3951 ns | 0.0024 |      40 B |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 5.9722E+24           |  46.723 ns |  15.6004 ns |  0.8551 ns | 0.0043 |      72 B |
| **Construct_FromDouble**                       | **True**        | **1.797(...)E+308 [23]** | **358.620 ns** |  **13.8776 ns** |  **0.7607 ns** | **0.0334** |     **560 B** |
| Construct_FromDoubleRounded                | True        | 1.797(...)E+308 [23] |  10.763 ns |   1.0092 ns |  0.0553 ns | 0.0091 |     152 B |
| Construct_FromDoubleRoundedToEightDigits   | True        | 1.797(...)E+308 [23] | 570.492 ns | 316.6851 ns | 17.3586 ns | 0.0181 |     304 B |
| Construct_FromDoubleRoundedToFifteenDigits | True        | 1.797(...)E+308 [23] | 583.981 ns |  83.6802 ns |  4.5868 ns | 0.0200 |     336 B |
| **Construct_FromDouble**                       | **True**        | **Infinity**             |   **6.757 ns** |   **0.0153 ns** |  **0.0008 ns** |      **-** |         **-** |
| Construct_FromDoubleRounded                | True        | Infinity             |   3.187 ns |   0.1247 ns |  0.0068 ns |      - |         - |
| Construct_FromDoubleRoundedToEightDigits   | True        | Infinity             |   3.939 ns |   0.1828 ns |  0.0100 ns |      - |         - |
| Construct_FromDoubleRoundedToFifteenDigits | True        | Infinity             |   3.918 ns |   0.0881 ns |  0.0048 ns |      - |         - |
