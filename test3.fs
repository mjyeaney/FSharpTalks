//
// Basic files representing a parse / aggregate pipeline.
// Later, we should see if this can be scaled out as a "push" 
// model across nodes.
//
open System
open System.IO
open System.Text

// Start with some basic definitions - I need to tighten these
// up as we move forward.
type KeyValue = string * string
type Key = string
type Operation = string
type AggregateFn = Operation * Key

// Core parsing pipeline - transform a sequence of bytes into 
// a stream of key/value pairs.
let ParseFile (fileStream:seq<byte>) =
    Seq.empty<KeyValue>

// Basic idea of the calculation engines. This is level-100 at the
// moment, but you get the idea.
let Process (values:seq<KeyValue>) (config:list<AggregateFn>) =
    list<string>.Empty

// Displays the output resulting from a pass through the Process 
// pipeline.
let DisplayOutput (summaryData:List<string>) =
    ()

// Utility methods
//
let WriteLogMessage (msg:string) =
    let timeStamp = DateTime.Now.ToString("u")
    let formattedMsg = String.Format("[{0}] INFO:: {1}\n", timeStamp, msg)
    File.AppendAllText(".\\log.txt", formattedMsg, Encoding.UTF8)

let LoadSampleFile =
    WriteLogMessage "Reading sample file..."
    let byteSeq = File.ReadAllBytes(".\\test1.fsx") |> Array.toSeq
    WriteLogMessage "Done!!!" |> ignore
    byteSeq

//
// Main method to kick things off
//
[<EntryPoint>]
let Main (args:string[]) =
    WriteLogMessage "Starting up..."
    match args with
        | [| filename |] -> 
            printfn "Input file: %s" filename
            WriteLogMessage "Found 1 argument..."
            LoadSampleFile |> ignore
        | _ -> printfn "Unrecognized number of arguments."
    
    WriteLogMessage "Shutting down..."
    0