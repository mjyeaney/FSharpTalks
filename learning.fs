//
// Discriminated Unions
//
type Expr =
| Binary of string * Expr * Expr
| Variable of string
| Constant of int;;

let v = Binary("+", Variable "x", Constant 10);;

let getVariableValue var =
    if (var = "x") then 10
    else failwith "Unknown variable!!!";;

let rec eval x =
    match x with
        | Binary(op, l, r) ->
            let (lv, rv) = (eval l, eval r)
            if (op = "+") then lv + rv
            elif (op = "-") then lv - rv
            elif (op = "*") then lv * rv
            elif (op = "/") then lv / rv
            else failwith "Unknown operator!!!"
        | Variable(var) ->
            getVariableValue var
        | Constant(n) ->
            n;;