#r "nuget: Npgsql"
#r "nuget: SqlProvider"

open System.IO
open FSharp.Data.Sql

let createMissingDir path =
    if not(Directory.Exists(path)) then
        Directory.CreateDirectory(path) |> ignore

let root = Path.Combine(Directory.GetCurrentDirectory(), ".public")
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
