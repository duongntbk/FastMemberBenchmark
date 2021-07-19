This is a benchmark of the [FastMember](https://github.com/mgravell/fast-member) library using the [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) tool.

I also wrote a blog post about FastMember, you can find it at the following link.
[https://duongnt.com/fastmember](https://duongnt.com/fastmember)

# Usage

Run the following command to start the benchmark.
```
dotnet run --configuration Release
```

You should then see the following list of tests
```
Available Benchmarks:
  #0 AccessorCreateRunner
  #1 PrivateGetRunner
  #2 PrivateSetRunner
  #3 PublicGetRunner
  #4 PublicSetRunner
```

Then select the test you want to run by typing its name or number. It's possible to run multiple tests back to back. For example, you can run `PrivateGetRunner` and and `PrivateSetRunner` by entering this.
```
1 2
```

Or

```
PrivateGetRunner PrivateSetRunner
```

# License

MIT License

https://opensource.org/licenses/MIT
