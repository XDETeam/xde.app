using System;
using System.CommandLine.Builder;
using System.CommandLine;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine.Parsing;
using System.Linq;
using Xde.Specs;
using Xde.Hosting;

var meta = Assembly.GetEntryAssembly().GetName();
Console.WriteLine($"{meta.Name} {meta.Version}");

//TODO:Try to put command hierarchy into DI
IServiceCollection services = new ServiceCollection();

//TODO: Scan and register commands
services.AddSingleton<Command, HostCommand>();
services.AddSingleton<HostCommand, HostCommand>();
services.AddSingleton<Command, WebDavCommand>();
services.AddSingleton<Command, SpecsCommand>();

//TODO:Implement the same for arguments/options? Or commands will be responsible for this?

var serviceProvider = services.BuildServiceProvider();
var commands = serviceProvider
	.GetServices<Command>()
	.Where( command => !command.Parents.Any() )
;

var rootCommand = new RootCommand();
foreach (var command in commands)
{
	rootCommand.AddCommand(command);
}

//TODO:How to force the second comand in "host ..."

var parser = new CommandLineBuilder(rootCommand)
	.UseDefaults()
	//TODO:
	//.UseHelp(ctx => ctx
	//	.HelpBuilder
	//	.CustomizeLayout(ConsoleExtensions.CustomHelpLayout)
	//)
	.Build()
;

return parser
	.Parse(args)
	.Invoke()
;

//TODO: AppOptions.Process(args);

/*
 * - Есть раздел Security | Account
 * - Этот раздел должен быть доступен через протокол HTTPS. При попытке HTTP-доступа,
 * осуществлять 302 Redirect.
 *   - Может быть введена категория "безопасных разделов"
 * - В рамках Security есть операция SignIn
 *   - Вводится логин и пароль (реквизиты)
 *   - Проверяется, что пользователь с такими реквизитами присутсвует в базе данных.
 *   - Редиректим на ReferrerUrl, если он был предоставлен и не входит в число некоторых
 *   системных, в противном случае ведём на дефолтную страницу.
 *   - Опция remember (persistent cookie)
 *   
 */

// RequestResponseIIdea.Run();

// var summary = BenchmarkRunner.Run<ReflectionBenchmark>();
// var summary = BenchmarkRunner.Run<ReflectionCacheBenchmark>();
// var summary = BenchmarkRunner.Run<RecurseGeneratorBenchmark>();
// var summary = BenchmarkRunner.Run<DictionaryBenchmark>();
