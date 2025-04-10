```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5371/22H2/2022Update)
AMD Ryzen 9 7900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method | Accuracy | Fraction          | Mean        | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------- |--------- |------------------ |------------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| **Sqrt**   | **15**       | **2/3**               |   **222.57 ns** |  **0.790 ns** |  **0.739 ns** |  **1.00** |    **0.00** | **0.0162** |     **272 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **2**                 |   **224.46 ns** |  **0.640 ns** |  **0.598 ns** |  **1.00** |    **0.00** | **0.0138** |     **232 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **3**                 |   **184.57 ns** |  **0.700 ns** |  **0.655 ns** |  **1.00** |    **0.00** | **0.0100** |     **168 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **2469/200**          |   **274.30 ns** |  **0.574 ns** |  **0.479 ns** |  **1.00** |    **0.00** | **0.0219** |     **368 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **1024**              |    **10.51 ns** |  **0.048 ns** |  **0.045 ns** |  **1.00** |    **0.01** |      **-** |         **-** |          **NA** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **183749/20**         |   **261.24 ns** |  **1.088 ns** |  **1.018 ns** |  **1.00** |    **0.01** | **0.0224** |     **376 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **15241578750190521** |   **155.83 ns** |  **1.326 ns** |  **1.175 ns** |  **1.00** |    **0.01** | **0.0119** |     **200 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **15**       | **15241578750190522** |   **156.34 ns** |  **3.115 ns** |  **6.221 ns** |  **1.00** |    **0.06** | **0.0138** |     **232 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **2/3**               |   **646.40 ns** |  **3.332 ns** |  **3.117 ns** |  **1.00** |    **0.01** | **0.0401** |     **672 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **2**                 |   **581.33 ns** |  **2.462 ns** |  **2.303 ns** |  **1.00** |    **0.01** | **0.0267** |     **456 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **3**                 |   **569.87 ns** |  **5.077 ns** |  **4.749 ns** |  **1.00** |    **0.01** | **0.0219** |     **376 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **2469/200**          |   **694.34 ns** |  **3.032 ns** |  **2.688 ns** |  **1.00** |    **0.01** | **0.0467** |     **792 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **1024**              |    **10.50 ns** |  **0.056 ns** |  **0.052 ns** |  **1.00** |    **0.01** |      **-** |         **-** |          **NA** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **183749/20**         |   **688.69 ns** |  **2.649 ns** |  **2.478 ns** |  **1.00** |    **0.00** | **0.0467** |     **792 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **15241578750190521** |   **312.99 ns** |  **1.848 ns** |  **1.729 ns** |  **1.00** |    **0.01** | **0.0248** |     **416 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **30**       | **15241578750190522** |   **322.61 ns** |  **1.683 ns** |  **1.492 ns** |  **1.00** |    **0.01** | **0.0224** |     **376 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **2/3**               |   **948.37 ns** |  **1.950 ns** |  **1.522 ns** |  **1.00** |    **0.00** | **0.0544** |     **912 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **2**                 |   **799.88 ns** |  **3.067 ns** |  **2.719 ns** |  **1.00** |    **0.00** | **0.0324** |     **552 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **3**                 |   **909.02 ns** |  **2.407 ns** |  **2.252 ns** |  **1.00** |    **0.00** | **0.0324** |     **552 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **2469/200**          |   **888.00 ns** |  **3.619 ns** |  **3.208 ns** |  **1.00** |    **0.00** | **0.0515** |     **864 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **1024**              |    **10.67 ns** |  **0.025 ns** |  **0.021 ns** |  **1.00** |    **0.00** |      **-** |         **-** |          **NA** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **183749/20**         |   **919.52 ns** |  **5.189 ns** |  **4.600 ns** |  **1.00** |    **0.01** | **0.0515** |     **864 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **15241578750190521** |   **342.32 ns** |  **0.649 ns** |  **0.575 ns** |  **1.00** |    **0.00** | **0.0300** |     **504 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **45**       | **15241578750190522** |   **527.65 ns** |  **3.646 ns** |  **3.411 ns** |  **1.00** |    **0.01** | **0.0324** |     **552 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **2/3**               | **2,069.30 ns** | **13.658 ns** | **12.107 ns** |  **1.00** |    **0.01** | **0.1221** |    **2064 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **2**                 | **1,781.43 ns** |  **9.729 ns** |  **8.625 ns** |  **1.00** |    **0.01** | **0.0744** |    **1248 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **3**                 | **1,875.07 ns** |  **4.798 ns** |  **4.254 ns** |  **1.00** |    **0.00** | **0.0648** |    **1104 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **2469/200**          | **1,971.44 ns** |  **8.327 ns** |  **7.382 ns** |  **1.00** |    **0.01** | **0.1259** |    **2136 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **1024**              |    **10.48 ns** |  **0.042 ns** |  **0.035 ns** |  **1.00** |    **0.00** |      **-** |         **-** |          **NA** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **183749/20**         | **2,158.74 ns** |  **7.043 ns** |  **6.243 ns** |  **1.00** |    **0.00** | **0.1335** |    **2280 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **15241578750190521** |   **566.71 ns** |  **5.466 ns** |  **5.113 ns** |  **1.00** |    **0.01** | **0.0696** |    **1176 B** |        **1.00** |
|        |          |                   |             |           |           |       |         |        |           |             |
| **Sqrt**   | **90**       | **15241578750190522** | **1,137.01 ns** | **10.969 ns** | **10.261 ns** |  **1.00** |    **0.01** | **0.0648** |    **1104 B** |        **1.00** |
