using Xunit;

namespace Xde.Forms.Schema
{
	public class FullnameSpecs
	{
		[Fact]
		public void Hash_ValidValues_ProperHash()
		{
			var name = FullnameSample.Default;
			Assert.Equal("LpTVypBjr4q+BSXPxnkTJAmp/LKogLhKSBUU3gz0Kdg=", name.Hash);
		}
	}
}
