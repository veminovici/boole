namespace Simplee.Intelligence

type Cell =
| Dead
| Live

[<RequireQualifiedAccess>]
module Conway =

    let isDead = function
    | Dead -> true
    | Live -> false

    let isLive = function
    | Dead -> false
    | Live -> true

    let countLive = List.sumBy (function | Live -> 1 | Dead -> 0)

    let transition cell count =
        match cell, count with
        | Live, 2
        | Live, 3 -> Live
        | Dead, 2 -> Live
        | _, _    -> Dead

    let next cell = countLive >> transition cell

    let unfold zro = 
        zro
        |> Seq.unfold (fun game -> Some(game, game))
