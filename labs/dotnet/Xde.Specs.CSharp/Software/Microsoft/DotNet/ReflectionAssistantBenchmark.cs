using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Xunit;
using static Xde.Software.Microsoft.DotNet.ReflectionAssistantSpecs;

namespace Xde.Software.Microsoft.DotNet
{
	public class ReflectionAssistantBenchmark
	{
		private Assembly _assembly;
		private ReflectionAssistant _assistant;

		[GlobalSetup]
		public void Setup()
		{
			_assembly = typeof(ReflectionAssistantBenchmark).Assembly;
			_assistant = new ReflectionAssistant()
				.AddTypes(_assembly)
				.Prepare()
			;
		}

		[Benchmark]
		public void PreapreAssistant()
		{
			_assistant = new ReflectionAssistant()
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
		public void LookupOpenGeneric()
		{
			var result = _assistant
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
