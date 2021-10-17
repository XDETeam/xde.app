using System.Collections.Generic;
using CommandLine;

namespace Xde.App
{
	public class AppOptions
	{
		//TODO:Replace with IoC'ed version
		public static object Process(IEnumerable<string> args) => Parser
			.Default
			.ParseArguments<WebDavCommand.Options>(args)
			.WithParsed<WebDavCommand.Options>(options => new WebDavCommand().Execute(options))
		;
	}
}
