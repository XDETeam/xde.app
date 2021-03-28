using System.Collections;
using System.Runtime.CompilerServices;

namespace Xde.Flow.Parse
{
	/// <summary>
	/// TODO:Parsers
	/// </summary>
	/// <remarks>
	/// <para>
	/// TODO:What is responsibility of the parser? Compared to different transformers,
	/// processorts, generators, etc. Probably parser is converting non-structured into
	/// structured (tokens into structures). Generator - vice versa. And transformers
	/// converting structures.
	/// </para>
	/// <para>
	/// Parser should not respond for generating DOM models. It can be streaming (e.g.
	/// JSONL).
	/// </para>
	/// <para>
	/// Using <see cref="IEnumerable"/> as input/ouput looks obvious. But performance is
	/// important for this.
	/// </para>
	/// </remarks>
	[CompilerGenerated]
	internal class NamespaceDoc
	{

	}
}
