namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Driver.Geometry
open AUTD3Sharp.Utils

module GroupByDeviceTest =
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Default()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        autd.Group(fun dev -> match dev.Idx with
                                | 0 ->  "null"
                                | 1 ->  "focus"
                                | _ ->  null
             )
            .Set("null", new Static(), new Null())
            .Set("focus", new Sine(150), new Focus(autd.Geometry.Center + new Vector3d(0,0,150)))
            .SendAsync()|> Async.AwaitTask |> Async.RunSynchronously |> ignore;

module GroupByTransducerTest =
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Default()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        let cx = autd.Geometry.Center.x;
        let g1 = new Focus(autd.Geometry.Center + Vector3d(0., 0., 150.));
        let g2 = new Null();

        let grouping (dev: Device) (tr: Transducer) =
            if (tr.Position.x < cx) then "focus" :> obj else "null" :> obj
        let g = (new Group(grouping)).Set("focus", g1).Set("null", g2);
        let m = new Sine(150);

        (m, g) |> autd.SendAsync  |> Async.AwaitTask |> Async.RunSynchronously |> ignore;