using System;
using Xde.Forms.Flow.Parse;
using Xde.Forms.Schema;
using Xunit;

namespace Xde.Forms.Code
{
	/// <summary>
	/// TODO:<see cref="SchemaBuilder"/> specs
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:It would be nice to implement abstractions for parsers, because most of tests
	/// will be the same. And maybe implement class-per-test scenario.
	/// </remarks>
	public class SchemaBuilderSpecs
	{
		public class Sample
		{
			public int Id { get; set; }

			public string Name { get; set; }
		}

		[Fact]
		public void Parse_TypeIsNull_ThrowException()
		{
			IParser<Type, Form> parser = new SchemaBuilder();
			
			var e = Assert.Throws<ArgumentNullException>(() => parser.Parse(null));

			Assert.Equal("type", e.ParamName);
		}

		[Fact]
		public void Parse_SampleType_ProperForm()
		{
			IParser<Type, Form> parser = new SchemaBuilder();
			
			var form = parser.Parse(typeof(Sample));

			Assert.NotNull(form);

			var type = typeof(Sample);
			Assert.Equal(type.Name, form.Fullname.Name);
			Assert.Equal(type.Namespace, form.Fullname.Namespace);
			Assert.Equal(type.AssemblyQualifiedName, form.Fullname.Layer);

			Assert.NotNull(form.Aspects);
			Assert.Collection(
				form.Aspects,
				//TODO:Add item.Form comparisons that should be reused from the registry
				item => Assert.Equal(nameof(Sample.Id), item.Name),
				item => Assert.Equal(nameof(Sample.Name), item.Name)
			);
		}
	}
}
