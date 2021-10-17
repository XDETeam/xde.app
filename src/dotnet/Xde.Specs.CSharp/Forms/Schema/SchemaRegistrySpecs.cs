using System;
using Xunit;

namespace Xde.Forms.Schema
{
	public class SchemaRegistrySpecs
	{
		[Fact]
		public void Get_NotExistingForm_ReturnNull()
		{
			var registry = new SchemaRegistry();
			var name = FullnameSample.Default;
			var form = registry.Get(name);
			Assert.Null(form);
		}

		[Fact]
		public void Add_AlreadyExistingForm_ThrowException()
		{
			var registry = new SchemaRegistry();
			var form = new Form(FullnameSample.Default);
			
			registry.Add(form);

			var e = Assert
				.Throws<InvalidOperationException>(() => registry.Add(form))
			;

			Assert.Equal("Already exists", e.Message);
		}

		[Fact]
		public void Get_WhenAdd_ReturnSame()
		{
			var registry = new SchemaRegistry();
			var form = new Form
			{
				Fullname = FullnameSample.Default
			};

			registry.Add(form);
			var actual = registry.Get(form.Fullname);
			Assert.Equal(form, actual);
		}
	}
}
