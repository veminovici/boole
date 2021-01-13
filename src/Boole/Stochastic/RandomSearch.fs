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
