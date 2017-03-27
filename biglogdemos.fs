open System.IO;;

let readData () =
    seq {
        let reader = new StreamReader("biglog.txt")
        while (not reader.EndOfStream) do
            yield reader.ReadLine() |> float
        reader.Dispose()
    };;

readData() |> Seq.average

let cachedData =
    readData() |> Seq.cache;;

cachedData |> Seq.average;;