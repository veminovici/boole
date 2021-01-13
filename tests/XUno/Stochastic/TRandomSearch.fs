namespace Simplee.Tests.Stochastic

module TRandomSearch =

    open Xunit
    open Xunit.Abstractions

    open Simplee.Intelligence.Stochastic

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
