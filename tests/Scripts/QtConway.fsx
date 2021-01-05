#load "../../src/Boole/Conway.fs"

open Simplee.Intelligence
open Simplee.Intelligence.Conway

//
// Tests
//

let tstConway () = 
    [| 
    Dead; Live; Dead; 
    Dead; Live; Dead; 
    Dead; Live; Dead|] 
    |> Board.ofCells 3 3
    |> nextBoard
    |> Board.pretty
    |> printfn "%s"

tstConway ()
