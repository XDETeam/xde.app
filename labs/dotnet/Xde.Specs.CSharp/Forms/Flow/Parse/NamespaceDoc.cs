using System.Collections;
using System.Runtime.CompilerServices;

namespace Xde.Forms.Flow.Parse
{
	/// <summary>
	/// TODO:Parsers
	/// </summary>
	/// <remarks>
	/// <para>
	/// TODO:What is responsibility of the parser? Compared to different transformers,
	/// processorts, generators, etc. Probably parser is converting non-structured into
	/// structured (tokens into structures). Generator - vice versa (maybe not a good
	/// idea because generators should create from the ground, maybe renderers). And
	/// transformers converting structures.
	/// </para>
	/// <para>
	/// Parser should not respond for generating DOM models. It can be streaming (e.g.
	/// JSONL).
	/// </para>
	/// <para>
	/// Using <see cref="IEnumerable"/> as input/ouput looks obvious. But performance is
	/// important for this.
	/// </para>
	/// <para>
	/// There is an opinion that parsers and validators are tightly coupled. Probably
	/// parsing is more important because it converts into structure and structure has a
	/// validity criterias. Or parser simply emits some messages as a response to signals?
	/// So acts like a decoder and the renderers become encoders?
	/// </para>
	/// <para>
	/// Parser should be reusable. And data behind parsers too. For example arithmetic
	/// operations should not be a part of every specific language.
	/// </para>
	/// </remarks>
	[CompilerGenerated]
	internal class NamespaceDoc
	{

	}
}
