using System.Linq;
using Xunit;

namespace Xde.Forms.Code
{
	public class ReflectionCacheSpecs
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

		public interface IValidator<T>
		{
			bool Validate(T model);
		}

		public interface IValidatedContract
		{

		}

		public class ValidatedRequest
			: IValidatedContract
		{

		}

		public class SomeValidator
			: IValidator<IValidatedContract>
		{
			bool IValidator<IValidatedContract>.Validate(IValidatedContract model) => true;
		}

		// TODO: If we have a IValidator<ISomeContract> and looking for IValidator<T>
		// where T is ISomeContract.
		[Fact]
		public void Lookup_ValidatorForSpecificType_ReturnsValidatorForContract()
		{
			var assistant = new ReflectionCache();
			assistant.AddTypes(GetType().Assembly);
			assistant.Prepare();

			var actual = assistant
				.Lookup(typeof(IValidator<ValidatedRequest>))
				.ToArray()
			;

			Assert.Single(actual);
			Assert.Equal(typeof(SomeValidator), actual.Single());
		}

		[Fact]
		public void Lookup_ExistingOpenGeneric_ReturnsValid()
		{
			var assistant = new ReflectionCache();
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
			var assistant = new ReflectionCache();
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
