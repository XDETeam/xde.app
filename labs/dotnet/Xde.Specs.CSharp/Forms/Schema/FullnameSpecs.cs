using Xunit;

namespace Xde.Forms.Schema
{
	public class FullnameSpecs
	{
		[Fact]
		public void Hash_ValidValues_ProperHash()
		{
			var name = new Fullname()
			{
				Name = "TypeName",
				Namespace = "Company.Domain.Subdomain",
				Layer = "Company.Core.dll"
			};

			Assert.Equal("LpTVypBjr4q+BSXPxnkTJAmp/LKogLhKSBUU3gz0Kdg=", name.Hash);
		}
	}
}
