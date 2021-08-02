using System.Runtime.CompilerServices;

namespace Xde.Qa
{
	/// <summary>
	/// Quality assurance
	/// </summary>
	/// 
	/// <remarks>
	/// - This can become a "Specs" namespace?
	/// - For specs there is one interesting requirement. Let's say we have some subsystem
	/// that have a bunch of tests/specs. And we want to compare several implementations of
	/// this subsystem. How to ensure that all tests were implemented. Abstract class with
	/// a lot of test methods is not a good option.
	/// - How Benchmarking can govern implementation? E.g. we have bench A better than B.
	/// Using this tests we made some implementations. But with new .NET version B was
	/// significantly improved and becomes faster. How can we change the implementation.
	/// Or we want to have both, depends on the platform.
	/// </remarks>
	[CompilerGenerated]
	internal class NamespaceDoc
	{

	}
}
