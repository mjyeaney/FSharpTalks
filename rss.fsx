open System;;
let rng = new Random();;
let n = (int)1e7;;
let data = 
    [1..n] |> Seq.map (fun x -> rng.NextDouble()) |> Seq.cache;;
let avg data =
    (/) (data |> Seq.sum) ((float)(Seq.length data));;
let sumOfSquares data =
    let mean = avg data
    data |> Seq.sumBy (fun x -> Math.Pow(mean - x, 2.0));;
let RSS data =
    Math.Sqrt(sumOfSquares data);;