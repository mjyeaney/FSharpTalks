open System
open Tutorials
open WebServer

[<EntryPoint>]
let main argv = 
    let myData = SumOfSquares.GetData ((int)1e5)
    let myDataSize = Seq.length myData
    printfn "You generated %i vaules" myDataSize

    WebServer.Start
    Console.Read() |> ignore
    0