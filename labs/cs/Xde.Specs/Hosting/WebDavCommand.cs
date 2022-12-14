using System.CommandLine;

namespace Xde.Hosting;

public class WebDavCommand
	: Command
{
	public WebDavCommand(HostCommand hostCommand)
		: base(Name, Description)
	{
		hostCommand.AddCommand(this);
	}

	public const string Name = "webdav";

	public const string Description = "Host webdav server";
}
