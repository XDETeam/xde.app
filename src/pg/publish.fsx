#r "nuget: Npgsql"
#r "nuget: SqlProvider"
#r "nuget: Microsoft.AspNetCore.Hosting"
#r "nuget: Microsoft.AspNetCore.Server.Kestrel"
#r "nuget: Microsoft.AspNetCore.StaticFiles"
#r "nuget: Microsoft.Extensions.FileProviders.Physical"

open System.IO
open FSharp.Data.Sql
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.FileProviders
open Microsoft.Extensions.Hosting

let PublicFolder = ".public"

(*
let createMissingDir path =
    if not(Directory.Exists(path)) then
        Directory.CreateDirectory(path) |> ignore

let root = Path.Combine(Directory.GetCurrentDirectory(), PublicFolder)
createMissingDir root

type Db = SqlDataProvider<Common.DatabaseProviderTypes.POSTGRESQL,
                           "Host=192.168.1.2;Username=postgres;Database=xde",
                           Owner = "mesh">
let context = Db.GetDataContext()

let publicNodes = 
    query { 
        for node in context.Mesh.NodeWebPublish do
            select node
    }
    |> Seq.iter (fun node ->
        let path = $"{root}/{node.Path.TrimStart('/')}.html"
        printfn "%s" path

        let folder = Path.GetDirectoryName(path)
        createMissingDir folder

        File.WriteAllText(path, node.Content)
    )
*)

(*
type Startup() = 

    member this.Configure(app: IApplicationBuilder) =
        let root = Path.Combine(Directory.GetCurrentDirectory(), PublicFolder)
        let files = new PhysicalFileProvider(root)
        let staticOptions = StaticFileOptions(FileProvider = files)

        let options = FileServerOptions(EnableDefaultFiles = true, EnableDirectoryBrowsing = true)
        app.UseFileServer(options) |> ignore
        //app.UseStaticFiles(staticOptions) |> ignore
        //app.UseDefaultFiles() |> ignore
        //app.UseDirectoryBrowser(DirectoryBrowserOptions(FileProvider = files)) |> ignore

        //TODO: app.Run(fun context -> context.Response.WriteAsync("Hello from ASP.NET Core!"))

let host = WebHostBuilder().UseKestrel().UseStartup<Startup>().Build()
host.Run()
*)

(*
let useFileServer (opts : FileServerOptions) (app : IApplicationBuilder) = 
    app.UseFileServer(opts)

let configureApp (app : IApplicationBuilder) = 
    
    let root = sprintf ".%cpublic" Path.DirectorySeparatorChar
    let opts = FileServerOptions(RequestPath = PathString.Empty, FileProvider = new PhysicalFileProvider(root))
            
    // set up app
    app
    |> useFileServer opts
    |> ignore

type Startup() =
    member x.Configuration (app: IApplicationBuilder) =
        configureApp(app) |> ignore
*)

// TODO:0 https://github.com/search?p=6&q=UseFileServer&type=Code

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddControllersWithViews().AddRazorRuntimeCompilation() |> ignore
        services.AddRazorPages() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =

        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            app.UseExceptionHandler("/Home/Error") |> ignore

        app.UseStaticFiles() |> ignore

        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllerRoute(
                name = "default",
                pattern = "{controller=Home}/{action=Index}/{id?}") |> ignore
            endpoints.MapRazorPages() |> ignore) |> ignore

    member val Configuration : IConfiguration = null with get, set

module Program =
    let exitCode = 0

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode
