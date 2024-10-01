using System.CommandLine;

namespace Xde.Specs;

public class SpecsCommand
	: Command
{
	public SpecsCommand()
		: base(Name, Description)
	{

	}

	public const string Name = "specs";

	public const string Description = "Specifications tools";
}
