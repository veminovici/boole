namespace Simplee.Intelligence

type Board<'T> = {
    Height: int
    Width:  int
    Cells:  'T [] }

[<RequireQualifiedAccess>]
module Board =

    let ofCells h w cs = {
        Height = h
        Width  = w
        Cells  = cs }

    let idx2xy b i =
        let y = i / b.Width
        let x = i % b.Width
        x, y

    let xy2idx b (x, y) =
        y * b.Width + x

    let boxIdxs (x, y) = [|
        x - 1, y - 1; x, y - 1; x + 1, y - 1
        x - 1, y; x + 1, y
        x - 1, y + 1; x, y + 1; x + 1, y + 1 |]

    let boxValues b d xys = 
        xys
        |> Array.map (function
        | x, _ when x < 0         -> d
        | x, _ when x >= b.Width  -> d
        | _, y when y < 0         -> d
        | _, y when y >= b.Height -> d
        | x, y                    -> let i = (x, y) |> xy2idx b in b.Cells |> Array.item i)

    let pretty b = 
        b.Cells
        |> Array.mapi (fun i c -> i, c)
        |> Array.groupBy (fun (i, _) -> i / b.Width)
        |> Array.map (snd >> Array.map (snd >> sprintf "%O") >> String.concat "")
        |> String.concat "\n"

    let nextBoard foldCell b = 
        let cs = b.Cells |> Array.mapi (foldCell b)
        { b with Cells = cs }

    let run foldCell zro =
        zro
        |> Seq.unfold (fun b -> let b' = nextBoard foldCell b in Some (b', b'))

module Conway = 

    type Cell = 
        | Dead
        | Live
        with
        override this.ToString() =
            match this with
            | Dead -> "o"
            | Live -> "x"

    let private foldCell c vals =
        vals 
        |> Array.sumBy (function | Dead -> 0 | Live -> 1)
        |> function
        | ls when c = Live && (ls = 2 || ls = 3) -> Dead
        | ls when c = Dead && ls = 3             -> Live
        | _                                      -> Dead

    let private nextCell b i c =
        i 
        |> Board.idx2xy b 
        |> Board.boxIdxs 
        |> Board.boxValues b Dead 
        |> foldCell c

    let run (zro: Board<Cell>) = Board.run nextCell zro
