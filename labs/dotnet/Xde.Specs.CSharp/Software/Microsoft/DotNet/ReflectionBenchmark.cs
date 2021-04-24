using BenchmarkDotNet.Attributes;
using System;
using System.Reflection;

namespace Xde.Software.Microsoft.DotNet
{
	public class ReflectionBenchmark
	{
		private static readonly Assembly _assembly = typeof(ReflectionBenchmark)
			.Assembly
		;

		private static readonly Type[] _types = _assembly.GetTypes();

		private static readonly Type[] _exportedTypes = _assembly.GetExportedTypes();

		[Benchmark]
		public Type[] GetExportedTypes() => _assembly.GetExportedTypes();

		[Benchmark]
		public Type[] GetCachedExportedTypes() => _exportedTypes;

		[Benchmark]
		public Type[] GetTypes() => _assembly.GetTypes();

		[Benchmark]
		public Type[] GetCachedTypes() => _types;
	}
}
