using Argon2id;
using BenchmarkDotNet.Running;

var result = BenchmarkRunner.Run<Benchmark>();
