open System
open System.Net
open System.Text
open System.IO

let siteRoot = "~/Projects/fsharp/"
let host = "http://localhost:8080/"

let listener (handler:(HttpListenerRequest->HttpListenerResponse->Async<unit>)) = 
    let hl = new HttpListener()
    hl.Prefixes.Add host
    hl.Start()

    let task = Async.FromBeginEnd(hl.BeginGetContext, hl.EndGetContext)
    async {
        while true do
            let! context = task
            Async.Start(handler context.Request context.Response)
    } |> Async.Start

let output (req:HttpListenerRequest) = 
    let reqPath = Uri(host).MakeRelativeUri(req.Url).OriginalString
    let file = Path.Combine(siteRoot, reqPath) 

    printfn "Request path: '%s'" reqPath
    printfn "Requested: '%s'" file

    if (File.Exists file)
        then File.ReadAllText(file)
        else "File does not exist!"

listener (fun req resp ->
    async {
        let txt = Encoding.UTF8.GetBytes(output req)
        resp.ContentType <= "text/html" |> ignore
        resp.OutputStream.Write(txt, 0, txt.Length)
        resp.OutputStream.Close()
    })

Console.WriteLine("Starting server on port 8080...")
Console.ReadLine() |> ignore
Console.WriteLine("Shutting down...")
