namespace Simplee.Tests

module TConway =

    open Simplee.Intelligence

    open Xunit
    open Xunit.Abstractions

    type Tests (output: ITestOutputHelper) =

        [<Fact>]
        let ``Cell isDead`` () =
            Dead
            |> Conway.isDead
            |> Assert.True

            Live
            |> Conway.isDead
            |> Assert.False

        [<Fact>]
        let ``Cell isLive`` () =
            Live
            |> Conway.isLive
            |> Assert.True

            Dead
            |> Conway.isLive
            |> Assert.False

        [<Fact>]
        let ``Cell countLive`` () =

            [Live; Dead; Live; Live; Dead]
            |> Conway.countLive
            |> (=) 3
            |> Assert.True

        [<Fact>]
        let ``Conway law L2`` () =
            (Live, 2)
            ||> Conway.transition
            |> (=) Live
            |> Assert.True

        [<Fact>]
        let ``Conway law L3`` () =
            (Live, 3)
            ||> Conway.transition
            |> (=) Live
            |> Assert.True

        [<Fact>]
        let ``Conway law D2`` () =
            (Dead, 2)
            ||> Conway.transition
            |> (=) Live
            |> Assert.True

        [<Fact>]
        let ``Conway law LD`` () =
            (Live, 1)
            ||> Conway.transition
            |> (=) Dead
            |> Assert.True

        [<Fact>]
        let ``Conway law DD`` () =
            (Dead, 3)
            ||> Conway.transition
            |> (=) Dead
            |> Assert.True

        [<Fact>]
        let ``Conway next`` () =
            let ns = [Live; Dead; Live]

            Live
            |> Conway.next ns
            |> (=) Live
            |> Assert.True
