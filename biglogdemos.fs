open System.IO;;

let readData () =
    seq {
        use reader = new StreamReader("biglog.txt")
        while (not reader.EndOfStream) do
            yield reader.ReadLine() |> float
    };;

#time;;
readData() |> Seq.average;;

let cachedData =
    readData() |> Seq.cache;;

cachedData |> Seq.average;;
cachedData |> Seq.average;;
#time;;