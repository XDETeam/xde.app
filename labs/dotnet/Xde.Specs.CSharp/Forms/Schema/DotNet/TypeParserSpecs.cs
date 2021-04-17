using Xunit;

namespace Xde.Forms.Schema.DotNet
{
	public class TypeParserSpecs
	{
		public class Sample
		{
			public int Id { get; set; }

			public string Name { get; set; }
		}

		[Fact]
		public void Parse_SampleType_ProperForm()
		{
			var parser = new TypeParser();
			var form = parser.Parse(typeof(Sample));
			Assert.NotNull(form);
		}
	}
}
