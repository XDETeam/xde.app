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
		private ReflectionCache _assistant;
		private ReflectionCacheUsingBitArray _assistantBitArray;

		[GlobalSetup]
		public void Setup()
		{
			_assembly = typeof(ReflectionCacheBenchmark).Assembly;
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

		[Benchmark(Baseline = true)]
		public void LookupRegular()
		{
			var result = _assembly
				.GetTypes()
				.Where(
					type => type
						.GetInterfaces()
						.Any(contract => contract == typeof(ISampleGenericContract<int>)
					)
				)
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
	}
}
