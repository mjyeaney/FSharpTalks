namespace Tutorials

open System

module SumOfSquares =
    let rng = Random()

    let GetData n = 
        [1..n] |> Seq.map (fun x -> rng.NextDouble()) |> Seq.cache

    let avg datum =
        (/) (datum |> Seq.sum) ((float)(Seq.length datum))

    let sumOfSquares datum =
        let mean = avg datum
        datum |> Seq.sumBy (fun x -> (mean - x) ** 2.0)

    let computeRss datum =
        Math.Sqrt(sumOfSquares datum)