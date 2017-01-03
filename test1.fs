open System

let rng = new Random()
let n = 1000

let data = [1..n] |> Seq.map(fun x -> rng.NextDouble())

let avg = (/) (data |> Seq.fold (+) 0.0) ((float) n)

printfn "Average = %f" avg

