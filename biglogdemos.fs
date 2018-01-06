open System.IO

let readData () =
    seq {
        use reader = new StreamReader("biglog.txt")
        while (not reader.EndOfStream) do
            yield reader.ReadLine() |> float
    }

let avg1 = readData() |> Seq.average

let cachedData =
    readData() |> Seq.cache

let avg2 = cachedData |> Seq.average
let avg3 = cachedData |> Seq.average

