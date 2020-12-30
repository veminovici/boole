open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks

let customCts = new CancellationTokenSource ()

// A bit redundant, trying to keep code simple
let cancelDefaultTokenAsync (sleepMs: int) = async {
    do! Async.Sleep sleepMs
    Async.CancelDefaultToken() }

// cancels tokens related to customCts
let cancelCustomTokenAsync (sleepMs: int) = async {
    do! Async.Sleep sleepMs
    customCts.Cancel() }

// wait for all tasks to be done and then we print Done
let whenAll (tasks: IEnumerable<Task>) =
    Task.WhenAll(tasks).ContinueWith(Action<Task>(fun _ -> printfn "Done"))

let infiltrators teamNumber = async {
    printfn "Infiltrator team %d: Starting" teamNumber
    for i in 1..5 do
        printfn "Team %d Infiltrating" teamNumber
        do! Async.Sleep 1000
    
    printfn "Team %d infiltration complete" teamNumber }

let surveillance teamNumber = async {
    printfn "Surveillance team %d: Starting" teamNumber
    do! infiltrators teamNumber
    for i in 1..5 do
        printfn "Team %d Surveilling" teamNumber
        do! Async.Sleep 1000
        
    printfn "Team %d surveillance complete" teamNumber }

let missionLeader teamNumber = async {
    printfn "Mission Leader %d: Starting Mission" teamNumber
    do! surveillance teamNumber
    for i in 1..5 do
        printfn "Leader %d Observing mission" teamNumber
        do! Async.Sleep 1000
        
    printfn "Leader %d mission complete" teamNumber }


let tstImplicitCt() =
    let t1 = missionLeader 1 |> Async.StartAsTask
    let cancelDefault = cancelDefaultTokenAsync 3000 |> Async.StartAsTask
    whenAll [t1; cancelDefault]

let tstCustomCt () =
    let t1 = Async.StartAsTask(missionLeader 1, cancellationToken=customCts.Token)
    let cancelCustom = cancelCustomTokenAsync 5000 |> Async.StartAsTask
    whenAll([t1; cancelCustom])

let tstCancelAll () =
    let t1 = infiltrators 1 |> Async.StartAsTask
    let t2 = infiltrators 2 |> Async.StartAsTask
    let cancelDefault = cancelDefaultTokenAsync 3000 |> Async.StartAsTask
    whenAll([t1; t2; cancelDefault])

let tstTasksAndTokens() =
    let t1 = infiltrators 1 |> Async.StartAsTask
    let t2 = Task.Delay(5000)
                 .ContinueWith(Func<Task, unit>(fun t -> printfn "Team 2 as task done"))
                 // needed to return Task<unit>
    let cancelDefault = cancelDefaultTokenAsync 3000 |> Async.StartAsTask
    whenAll([t1; t2; cancelDefault])

let tstTasksAndTokens1() =
    let t1 = infiltrators 1 |> Async.StartAsTask
    let t2 = Task.Delay(5000, Async.DefaultCancellationToken).ContinueWith(Func<Task, unit>(fun t -> printfn "Team 2 as task done"))
    let cancelDefault = cancelDefaultTokenAsync 3000 |> Async.StartAsTask
    whenAll([t1; t2; cancelDefault])

let linkedCts = CancellationTokenSource.CreateLinkedTokenSource(Async.DefaultCancellationToken)

let cancelLinkedTokenAsync (sleepMs: int) = async {
    do! Async.Sleep sleepMs
    linkedCts.Cancel() }

let tstLinkedDefault() =   
    let t1 = Async.StartAsTask(infiltrators 1, cancellationToken=linkedCts.Token)
    let cancelDefault = cancelDefaultTokenAsync 3000 |> Async.StartAsTask
    whenAll([t1; cancelDefault])

let tstSub() =
    // cancels tokens related to linkedCts
    let t1 = Async.StartAsTask(infiltrators 1, cancellationToken=linkedCts.Token)
    let t2 = infiltrators 2 |> Async.StartAsTask
    let cancelLinked = cancelLinkedTokenAsync 3000 |> Async.StartAsTask // notice we're cancelling only the linked one here!
    whenAll([t1; t2; cancelLinked])