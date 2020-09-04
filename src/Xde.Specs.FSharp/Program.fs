open System
open System.Reflection

[<EntryPoint>]
let main argv =
    printfn "XDE Specs %s" (Assembly.GetEntryAssembly().GetName().Version.ToString())
    0 // return an integer exit code