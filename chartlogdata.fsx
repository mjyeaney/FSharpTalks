#r ".\\Packages\\FSharp.Charting.0.90.14\\lib\\net40\\FSharp.Charting.dll";;

open System;;
open System.IO;;
open System.Text;;
open FSharp.Charting;;

let fileName = "agentlog.txt"

let readLogData fileName =
    let lines = File.ReadAllLines(fileName, Encoding.UTF8)
    lines |> Array.map (fun aLine -> Convert.ToDouble(aLine));;

let runtimePoints = readLogData fileName;;

let pointCount = runtimePoints |> Array.length;;

let runtimeSeries = Chart.Line(runtimePoints, Name="Log file data");;

let average (data:float[]) =
    (/) (data |> Array.sum) ((float)(data |> Array.length));;

let projectRps (avgTime:float) (totalRecords:int) =
    (/) (1000.0) (avgTime) |> (*) (totalRecords |> float);;

let averageValue = average runtimePoints;;

let averagePoints = [for x in 0 .. pointCount -> averageValue];;

let averageSeries = Chart.Line averagePoints;;

let combo = Chart.Combine([runtimeSeries; averageSeries;]);;


combo.ShowChart();;