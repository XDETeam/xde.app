using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xde
{
	public class TempSyntaxReceiver : ISyntaxReceiver
	{
		public List<string> Units { get; } = new List<string>();

		void ISyntaxReceiver.OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			//TODO:
			//if (!System.Diagnostics.Debugger.IsAttached)
			//{
			//	System.Diagnostics.Debugger.Launch();
			//}

			if (syntaxNode is CompilationUnitSyntax unit)
			{
				var path = unit.SyntaxTree.FilePath;
				if (path.EndsWith("Generated.cs"))
				{
					Units.Add(path);
				}
			}
		}
	}

	[Generator]
	public class TempGenerator : ISourceGenerator
	{
		void ISourceGenerator.Execute(GeneratorExecutionContext context)
		{
			var syntaxReceiver = (TempSyntaxReceiver)context.SyntaxReceiver;

			//TODO: Sample of creating SourceText
			//context.AddSource("TypeName.Generated.cs", ...source code...);

			//TODO: Sample of producing a file
			//foreach (var path in syntaxReceiver.Units)
			//{
			//	var additionalPath = System.IO.Path.ChangeExtension(path, ".Generated.sql");
			//	System.IO.File.WriteAllText(additionalPath, "TODO:");
			//}
		}

		void ISourceGenerator.Initialize(GeneratorInitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new TempSyntaxReceiver());
		}
	}
}
