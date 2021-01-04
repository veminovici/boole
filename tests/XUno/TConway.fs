namespace Simplee.Tests

module TConway =

    open Simplee.Intelligence

    open Xunit
    open Xunit.Abstractions

    type Tests (output: ITestOutputHelper) =

        [<Fact>]
        let ``Board ofCells`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]

            let b = Board.ofCells h w cs

            b.Cells
            |> (=) cs
            |> Assert.True

            b.Height
            |> (=) h
            |> Assert.True

            b.Width
            |> (=) w
            |> Assert.True

        [<Fact>]
        let ``Board idx2xy`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]
            let b = Board.ofCells h w cs

            let i = 4 
            let xy = Board.idx2xy b i

            xy
            |> fst
            |> (=) 1
            |> Assert.True

            xy
            |> snd
            |> (=) 1
            |> Assert.True

        [<Fact>]
        let ``Board idx2xy`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]
            let b = Board.ofCells h w cs

            let i = 4 
            let i' = i |> Board.idx2xy b |> Board.xy2idx b

            i
            |> (=) i'
            |> Assert.True

        [<Fact>]
        let ``Board boxIdx`` () =

            Board.boxIdxs (0, 0)
            |> Array.length
            |> (=) 8
            |> Assert.True

        [<Fact>]
        let ``Board boxValues`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]
            let b = Board.ofCells h w cs

            [|
                -1, 0
                3, 0 
                0, -1 
                0, 2 
                1, 1
            |]
            |> Board.boxValues b 100
            |> (=) [|100; 100; 100; 100; 4 |]

        [<Fact>]
        let ``Board pretty`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]
            let b = Board.ofCells h w cs

            b
            |> Board.pretty
            |> (<>) ""
            |> Assert.True

        [<Fact>]
        let ``Board nextBoard`` () =
            let h = 2
            let w = 3
            let cs = [|1..6|]
            let b = Board.ofCells h w cs

            b
            |> Board.nextBoard (fun b i c -> c * 10)
            |> (fun b -> b.Cells)
            |> (=) [|10;20;30;40;50;60|]
            |> Assert.True

        [<Fact>]
        let ``Conway nextBoard`` () =
            let h = 3
            let w = 3
            let cs = [|
                Conway.Dead; Conway.Live; Conway.Dead
                Conway.Dead; Conway.Live; Conway.Dead
                Conway.Dead; Conway.Live; Conway.Dead |]
            let b = Board.ofCells h w cs

            b
            |> Conway.nextBoard
            |> (fun b -> b.Cells)
            |> (=) [| 
                Conway.Dead;Conway.Dead;Conway.Dead
                Conway.Live;Conway.Dead;Conway.Live
                Conway.Dead;Conway.Dead;Conway.Dead|]
            |> Assert.True
