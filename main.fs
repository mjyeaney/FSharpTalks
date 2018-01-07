open System
open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Queue
//open Tutorials
//open WebServer

[<EntryPoint>]
let main argv = 
    // let myData = SumOfSquares.GetData ((int)1e5)
    // let myDataSize = Seq.length myData
    // printfn "You generated %i vaules" myDataSize

    // WebServer.Start
    // Console.Read() |> ignore
    let storageConnectionString = "--TODO--"

    let account = CloudStorageAccount.Parse storageConnectionString
    let serviceClient = account.CreateCloudQueueClient()

    Console.WriteLine "Creating Queue if not found"
    let queue = serviceClient.GetQueueReference "test"
    queue.CreateIfNotExistsAsync().Wait()

    Console.WriteLine "Sending message to queue.."
    let message = CloudQueueMessage("This is unbelievably still another test message")
    queue.AddMessageAsync(message).Wait()

    Console.WriteLine "Press <Enter> to read message back from queue..."
    Console.ReadLine() |> ignore

    Console.WriteLine "Reading messages from queue..."

    let mutable messageTask = queue.GetMessageAsync()
    messageTask.Wait()
    let mutable messageBody = messageTask.Result

    while not (isNull messageBody) do
        queue.DeleteMessageAsync(messageBody).Wait()
        Console.WriteLine ("Message is: " + messageBody.AsString)
        
        messageTask <- queue.GetMessageAsync()
        messageTask.Wait()
        messageBody <- messageTask.Result

    0