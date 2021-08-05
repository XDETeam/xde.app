using System;
using CommandLine;

namespace Xde.App
{
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
			Console.WriteLine(options.Test);
		}
	}
}
