#r "System.Windows.Forms.DataVisualization"
#r ".\\Packages\\FSharp.Charting.0.90.14\\lib\\net40\\FSharp.Charting.dll";;

open System;;
open System.IO;;
open System.Text;;
open FSharp.Charting;;
let fileName = "agentlog2.txt"
let readLogData fileName =
    let lines = File.ReadAllLines(fileName, Encoding.UTF8)
    lines |> Array.map (fun aLine -> Convert.ToDouble(aLine));;

let runtimePoints = readLogData fileName;;
let pointCount = runtimePoints |> Array.length;;
let runtimeSeries = Chart.Line(runtimePoints, Name="Log file data");;
let average (data:float[]) = data |> Array.average;;
let projectRps (avgTime:float) (totalRecords:int) =
    (/) (1000.0) (avgTime) |> (*) (totalRecords |> float);;
let averageValue = average runtimePoints;;
let stdDev (data:float[]) = Math.Sqrt <| (/) (data |> Array.map (fun x -> (x-averageValue) ** 2.0) |> Array.sum) ((float)pointCount)
let stdDevValue = stdDev runtimePoints;;

let averagePoints = [for x in 0 .. pointCount -> averageValue];;

let posStdDevPoints = [for x in 0 .. pointCount -> (averageValue + stdDevValue)]

let negStdDevPoints = [for x in 0 .. pointCount -> (averageValue - stdDevValue)]

let averageSeries = Chart.Line averagePoints;;

let posStdDevSeries = Chart.Line posStdDevPoints;;

let negStdDevSeries = Chart.Line negStdDevPoints;;

let combo = Chart.Combine([runtimeSeries; averageSeries; posStdDevSeries; negStdDevSeries;]);;

let showChart () =
    combo.ShowChart();;