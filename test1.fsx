//
// Experimental code file for playing around with basic syntax. 
//
open System

//
// Fold and average some uniform random numbers.
//
let rng = Random()
let n = 1000000
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
let s = "654874512"
printfn "Sum of digits in %s is %i" s (sumDigits s)