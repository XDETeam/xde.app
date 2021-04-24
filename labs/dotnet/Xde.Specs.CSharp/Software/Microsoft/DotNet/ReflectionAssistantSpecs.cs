using System.Linq;
using Xunit;

namespace Xde.Software.Microsoft.DotNet
{
	public class ReflectionAssistantSpecs
	{
		public interface ISampleContract
		{

		}

		public interface ISampleGenericContract<T>
		{

		}

		public class SampleInstance
			: ISampleContract
			, ISampleGenericContract<int>
		{

		}

		[Fact]
		public void Lookup_ExistingOpenGeneric_ReturnsValid()
		{
			var assistant = new ReflectionAssistant();
			assistant.AddTypes(GetType().Assembly);
			assistant.Prepare();

			var actual = assistant
				.Lookup(typeof(ISampleGenericContract<>))
				.ToArray()
			;

			Assert.Single(actual);
			Assert.Equal(typeof(SampleInstance), actual.Single());
		}

		[Fact]
		public void Lookup_ExistingGeneric_ReturnsValid()
		{
			var assistant = new ReflectionAssistant();
			assistant.AddTypes(GetType().Assembly);
			assistant.Prepare();

			var actual = assistant
				.Lookup(typeof(ISampleGenericContract<int>))
				.ToArray()
			;

			Assert.Single(actual);
			Assert.Equal(typeof(SampleInstance), actual.Single());
		}
	}
}
