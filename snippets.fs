//
// Tuples
//
let t = (42, "Hello");;
let (num, str) = t;;

//
// Records
//
type Product = {
    Name:string
    Price:float
};;

let p = { Name="Test"; Price=128.80 };;
let p2 = { p with Name = "Another" };;

//
// Structural Comparison of Types
//

let p3 = { Name="Test"; Price=128.80 };;
p = p3; // True!!!

//
// Discriminated Unions
//
type Expr =
| Binary of string * Expr * Expr
| Variable of string
| Constant of int;;

let v = Binary("+", Variable "x", Constant 10);;

let getVariableValue var =
    if (var = "x") then 10
    else failwith "Unknown variable!!!";;

let rec eval x =
    match x with
        | Binary(op, l, r) ->
            let (lv, rv) = (eval l, eval r)
            if (op = "+") then lv + rv
            elif (op = "-") then lv - rv
            elif (op = "*") then lv * rv
            elif (op = "/") then lv / rv
            else failwith "Unknown operator!!!"
        | Variable(var) ->
            getVariableValue var
        | Constant(n) ->
            n;;

//
// Lists & data pipelining
//

let d = [1;2;3;4;5;];;
List.map (fun x -> x * x) d;;
List.filter (fun x -> x % 2 = 0) d;;
List.sum d;;

d |> List.filter (fun x -> x % 2 = 0) |> List.map (fun x -> x * x) |> List.sum;;

// 
// Functions and partial application
//

let add a b = a + b;;
let add10 = add 10;;

// 
// Fun with pattern matching
//

type Variant =
| Num of int
| Str of string;;

let vn = Num(42);;
let vs = Str("Are we having fun yet???");;

let printIt v =
    match v with
        | Num(n) -> printfn "Number: %i" n
        | Str(s) -> printfn "String: %s" s;;

printIt vn;;
printIt vs;;

printIt <| Num(42);;
Str("Pipelining!!!") |> printIt;;

type Variant with
    member x.Print() = printIt x;;

//
// Actor Exmples
//


#nowarn "40"
let printerAgent = MailboxProcessor.Start(fun inbox-> 
    // the message processing function
    let rec messageLoop = async{
        
        // read a message
        let! msg = inbox.Receive()
        
        // process a message
        printfn "message is: %s" msg

        // loop to top
        return! messageLoop  
        }

    // start the loop 
    messageLoop 
    );;

// 
// Type Provider Example
//

#r "FSharp.Data.2.1.1/lib/net40/FSharp.Data.dll";;
open FSharp.Data;;

type MarketingData = CsvProvider<"sample_data/marketing.csv">;;
let data = MarketingData.Load("sample_data/marketing.csv");;
data.Rows |> Seq.head;;

let firstRow = data.Rows |> Seq.head;;
firstRow.Age;;
firstRow.``Buy CD``;;


