using System.Runtime.CompilerServices;

namespace Xde.Forms.Schema
{
	/// <summary>
	/// Data schemas
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Defines forms for data schemas and conversion routines (get schema from
	/// some source or generate schema into other format).
	/// </remarks>
	/// <todo>
	/// - Move DotNet.TypeParser to Xde.Software.
	/// - SchemaRegistry can be simply Schema. And TypeParser can be simply DotNetParser
	/// or DotNetRuntimeParser or *Builder. Layers/Namespaces can be references instead
	/// of strings. So no tricky dealing with using SchemaRegistry from TypeParser.
	/// - <see cref="TypeParser"/> should use SchemaRegistry.
	/// - Think about other parsers samples (SqlSchema for INFORMATION_SCHEMA, AVRO
	/// schema, .NET sources parser, etc.
	/// - Generators (SQL - different tastes, AVRO Schema, KSQL streams, etc).
	/// - Types (forms) mapping to convert from .NET to SQL for example.
	/// - Comparators (.NET class and SQL table)
	/// - Parsers generators should be located in Xde.Software.
	/// - Comments/docs
	/// </todo>
	[CompilerGenerated]
	internal class NamespaceDoc
	{

	}
}
