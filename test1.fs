//
// Experimental code file for playing around with basic syntax. 
//
open System

//
// Fold and average some uniform random numbers.
//
let rng = new Random()
let n = 1000
let data = [1..n] |> Seq.map(fun x -> rng.NextDouble())
let avg = (/) (data |> Seq.fold (+) 0.0) ((float) n)

printfn "Average = %f" avg

//
// Playing around with function decomposition
//
let mapCharToInt (x:char) =
    (-) (x |> Convert.ToInt32) 48
let textNumberToArray (x:string) =
    x.ToCharArray() |> Seq.map mapCharToInt
let sumDigits (num:string) =
    textNumberToArray num |> Seq.sum

printfn "Sum of digits in 555 is %i" (sumDigits "555")