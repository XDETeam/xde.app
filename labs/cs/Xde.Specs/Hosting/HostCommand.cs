using System.CommandLine;

namespace Xde.Hosting;

public class HostCommand
	: Command
{
	public HostCommand()
		: base(Name, Description)
	{
		
	}

	public const string Name = "host";

	public const string Description = "Run host application";
}
