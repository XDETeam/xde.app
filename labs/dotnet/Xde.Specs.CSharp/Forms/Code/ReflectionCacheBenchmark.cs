using System;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Xunit;
using static Xde.Forms.Code.ReflectionCacheSpecs;

namespace Xde.Forms.Code
{
	public class ReflectionCacheBenchmark
	{
		private Assembly _assembly;
		private Type[] _types;
		private ReflectionCache _assistant;
		private ReflectionCacheUsingBitArray _assistantBitArray;

		[GlobalSetup]
		public void Setup()
		{
			_assembly = typeof(ReflectionCacheBenchmark).Assembly;
			_types = _assembly.GetTypes().ToArray();
			_assistant = new ReflectionCache()
				.AddTypes(_assembly)
				.Prepare()
			;
			_assistantBitArray = new ReflectionCacheUsingBitArray()
				.AddTypes(_assembly)
				.Prepare()
			;
		}

		[Benchmark]
		public void PrepareAssistant()
		{
			_assistant = new ReflectionCache()
				.AddTypes(_assembly)
				.Prepare()
			;
		}

		[Benchmark]
		public void PrepareBitArrayAssistant()
		{
			_assistantBitArray = new ReflectionCacheUsingBitArray()
				.AddTypes(_assembly)
				.Prepare()
			;
		}

		[Benchmark]
		public void LookupGeneric()
		{
			var result = _assistant
				.Lookup(typeof(ISampleGenericContract<int>))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupBitArrayGeneric()
		{
			var result = _assistantBitArray
				.Lookup(typeof(ISampleGenericContract<int>))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupOpenGeneric()
		{
			var result = _assistant
				.Lookup(typeof(ISampleGenericContract<>))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupBitArrayOpenGeneric()
		{
			var result = _assistantBitArray
				.Lookup(typeof(ISampleGenericContract<>))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupSimpleCache()
		{
			var result = _types
				.Where(type => typeof(ISampleGenericContract<int>).IsAssignableFrom(type))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark(Baseline = true)]
		public void LookupRegular()
		{
			var result = _assembly
				.GetTypes()
				.Where(type => typeof(ISampleGenericContract<int>).IsAssignableFrom(type))
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupRegularOpenGeneric()
		{
			var result = _assembly
				.GetTypes()
				.Where(
					type => type.GetInterfaces().Any(contract
						=> contract.IsGenericType
						&& contract.GetGenericTypeDefinition() == typeof(ISampleGenericContract<>)
					)
				)
				.ToArray()
			;

			Assert.Single(result);
		}

		[Benchmark]
		public void LookupSimpleCacheOpenGeneric()
		{
			var result = _types
				.Where(
					type => type.GetInterfaces().Any(contract
						=> contract.IsGenericType
						&& contract.GetGenericTypeDefinition() == typeof(ISampleGenericContract<>)
					)
				)
				.ToArray()
			;

			Assert.Single(result);
		}
	}
}
