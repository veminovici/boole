namespace Simplee.Intelligence.Stochastic

open Simplee
open Simplee.State.ComputationExpression

[<RequireQualifiedAccess>]
module StochasticSearch = 

    /// run this function each iteration.
    let private iteration (newSln: State<'TState, 'TSln>) (cmpSln: 'TSln -> State<'TState, 'TSln>) = _state {
        let! sln = newSln
        let! bst = cmpSln sln
        return bst }

    let run newSln cmpSln = 
        State.unfold (iteration newSln cmpSln)

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
