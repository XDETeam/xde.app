using Xunit;

namespace Xde.Forms.Code
{
	public class ReflectSpecs
	{
		public class Sample { }

		public class GenericDefintionSample<T> { }

		public abstract class AbstractSample { }

		public class ParametrizedSample
		{
			public ParametrizedSample(int x, int y) { }
		}

		[Fact]
		public void CanNew_SimpleType_ReturnTrue()
		{
			var type = typeof(Sample);

			Assert.True(Reflect.CanNew(type));
		}

		[Fact]
		public void CanNewGeneric_SimpleType_ReturnFalse()
		{
			Assert.True(Reflect.CanNew<Sample>());
		}

		[Fact]
		public void CanNew_GenericDefintionType_ReturnFalse()
		{
			var type = typeof(GenericDefintionSample<>);

			Assert.False(Reflect.CanNew(type));
		}

		[Fact]
		public void CanNew_GenericType_ReturnTrue()
		{
			var type = typeof(GenericDefintionSample<>)
				.MakeGenericType(typeof(int))
			;

			Assert.True(Reflect.CanNew(type));
		}

		[Fact]
		public void CanNew_AbstractType_ReturnFalse()
		{
			var type = typeof(AbstractSample);

			Assert.False(Reflect.CanNew(type));
		}

		[Fact]
		public void CanNewGeneric_AbstractType_ReturnFalse()
		{
			Assert.False(Reflect.CanNew<AbstractSample>());
		}

		[Fact]
		public void CanNew_ParametrizedType_ReturnFalse()
		{
			var type = typeof(ParametrizedSample);

			Assert.False(Reflect.CanNew(type));
		}

		[Fact]
		public void CanNewGeneric_ParametrizedType_ReturnFalse()
		{
			Assert.False(Reflect.CanNew<ParametrizedSample>());
		}
	}
}
