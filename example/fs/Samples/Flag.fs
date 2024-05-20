namespace Samples

open AUTD3Sharp
open System
open System.Threading.Tasks
open type AUTD3Sharp.Units

module FlagTest =
    let Test<'T> (autd : Controller<'T>) = 
        printfn "press any key to run fan..."
        System.Console.ReadKey true |> ignore;

        (new ForceFan(fun dev -> true), new ReadsFPGAState(fun dev -> true)) |> autd.Send;

        let mutable fin = false;
        let th : Task =
            async {
                let prompts = [|'-'; '/'; '|'; '\\'|]
                let mutable promptsIdx = 0;
                while not fin do
                    let states = autd.FPGAState()
                    printfn "%c FPGA Status..." prompts.[promptsIdx / 1000 % prompts.Length]
                    printfn "%s" (String.Join("\n", states))
                    printf "\x1b[%dA" (states.Length + 1)
                    promptsIdx <- promptsIdx + 1
                done
            } |> Async.StartAsTask :> Task
       
        printfn "press any key stop checking FPGA status..."
        System.Console.ReadKey true |> ignore;

        fin <- true;
        th.Wait();
        
        (new ForceFan(fun dev -> false), new ReadsFPGAState(fun dev -> false)) |> autd.Send;

