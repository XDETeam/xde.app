using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Xde.Forms.Code
{
	public class ReflectionBenchmark
	{
		private static readonly Assembly _assembly = typeof(ReflectionBenchmark)
			.Assembly
		;

		private static readonly Type[] _types = _assembly.GetTypes();

		private static readonly Type[] _exportedTypes = _assembly.GetExportedTypes();

		private static readonly Type[] _publicAndNonAbstract = _types
			.Where(type => type.IsPublic)
			.Where(type => !type.IsAbstract)
			.ToArray()
		;

		[Benchmark]
		public Type[] GetTypes() => _assembly.GetTypes();

		[Benchmark(Baseline = true)]
		public Type[] GetExportedTypes() => _assembly.GetExportedTypes();

		[Benchmark]
		public Type[] GetDefinedTypes() => _assembly.DefinedTypes.ToArray();

		[Benchmark]
		public Type[] GetCachedTypes() => _types;

		[Benchmark]
		public Type[] GetCachedExportedTypes() => _exportedTypes;

		[Benchmark]
		public Type[] GetNonAbstract() => _assembly
			.GetExportedTypes()
			.Where(type => !type.IsAbstract)
			.ToArray()
		;

		[Benchmark]
		public Type[] GetCachedNonAbstract() => _publicAndNonAbstract;
	}
}
