namespace Simplee.Tests.Stochastic

module TRandomSearch =

    open Xunit
    open Xunit.Abstractions

    open Simplee
    open Simplee.Intelligence.Stochastic

    type RSState = {
        Best: int
        Current: int list }

    [<RequireQualifiedAccess>]
    module RandomSearch = 

        // We just increment a counter.
        let private newSolution = State <| fun (stt: RSState) ->
            let sln = List.length stt.Current
            sln, {stt with Current = stt.Current @ [sln] }

        // The best solution, is the biggest int that divides with 5.
        let private cmpSolution sln = State <| fun (stt: RSState) ->
            match sln with
            | sln when sln > stt.Best && sln % 5 = 0 -> sln, {stt with Best = sln }
            | _                                      -> stt.Best, stt

        let run = StochasticSearch.run newSolution cmpSolution

    type Tests (output: ITestOutputHelper) =

        [<Fact>]
        let ``RandomSearch run`` () =

            { Best = 0; Current = [] }
            |> RandomSearch.run
            |> Seq.take 10
            |> Seq.groupBy id
            |> Seq.take 1
            |> Seq.head
            |> fun (i, xs) ->
                i
                |> (=) 0
                |> Assert.True

                xs
                |> Seq.length
                |> (=) 5
                |> Assert.True
