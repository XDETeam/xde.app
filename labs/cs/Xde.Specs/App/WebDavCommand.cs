using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Xde.App;

public class WebDavCommand
{
	public const string CommandName = "webdav";

	[Verb(CommandName, HelpText = "WebDAV test server")]
	public class Options
	{
		[Option("test")]
		public string Test { get; set; }
	}

	public void Execute(Options options)
	{
		Host
			.CreateDefaultBuilder()
			.ConfigureWebHostDefaults(builder =>
			{
				builder
					//TODO:.UseKestrel()
					.UseStartup<WebDavStartup>()
				;
			})
			.Start()
		;
	}
}
