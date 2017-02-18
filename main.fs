open System
open System.Net
open System.Text
open System.IO

let host = "http://localhost:5555/"

let listener handler =
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
    let body = match req.RawUrl with
        | "/Test" -> "Just a test"
        | _ -> "Lovely little server, yea?"
    body

listener (fun req resp ->
    async {
        let status = match req.RawUrl with
            | "/favicon.ico" -> 404
            | _ -> 200
        let txt = Encoding.UTF8.GetBytes(output req)
        resp.ContentType <- "text/plain"
        resp.StatusCode <- status
        Console.WriteLine("Tried to set status {0}", resp.StatusCode)
        resp.OutputStream.Write(txt, 0, txt.Length)
        resp.OutputStream.Close()
        resp.Close()
    }
)