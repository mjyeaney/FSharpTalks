open System
open System.Net
open System.Text
open System.IO
open Newtonsoft.Json

let port = 5555
let host = String.Format("http://127.0.0.1:{0}/", port)

let WriteLogMessage (msg:string) =
    let timeStamp = DateTime.Now.ToString("G")
    let formattedMsg = String.Format("[{0}] INFO:: {1}\n", timeStamp, msg)
    //File.AppendAllText(".\\log.txt", formattedMsg, Encoding.UTF8)
    Console.Write(formattedMsg)

let ListenerHandler handler =
    let hl = new HttpListener()
    hl.Prefixes.Add host
    hl.Start()

    let task = Async.FromBeginEnd(hl.BeginGetContext, hl.EndGetContext)
    async {
        while true do
            let! context = task
            Async.Start(handler context.Request context.Response)
    } |> Async.Start

let GetResponseContent (req:HttpListenerRequest) =
    match req.RawUrl with
    | "/Test" -> (JsonConvert.SerializeObject(DateTime.UtcNow), "application/json")
    | _ -> ("Default content blurb...maybe a document or some markup", "text/plain")

ListenerHandler (fun req resp ->
    async {
        let status = 
            match req.RawUrl with
            | "/favicon.ico" -> 404
            | _ -> 200
        let (content, contentType) = GetResponseContent req
        let txt = Encoding.UTF8.GetBytes(content)
        resp.ContentType <- contentType
        resp.StatusCode <- status
        WriteLogMessage(String.Format("{0} - {1}", req.RawUrl, resp.StatusCode))
        resp.OutputStream.Write(txt, 0, txt.Length)
        resp.OutputStream.Close()
        resp.Close()
    }
)

WriteLogMessage(String.Format("Started server on port {0}", port))
Console.Read() |> ignore
WriteLogMessage("Shutting down...")