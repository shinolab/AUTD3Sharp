namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open type AUTD3Sharp.Units

module GroupByDeviceTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        autd.Group(fun dev -> match dev.Idx with
                                | 0 ->  "null"
                                | 1 ->  "focus"
                                | _ ->  null
             )
            .Set("null", new Static(), new Null())
            .Set("focus", new Sine(150u * Hz), new Focus(autd.Geometry.Center + new Vector3d(0,0,150)))
            .Send();

module GroupByTransducerTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let cx = autd.Geometry.Center.X;
        let g1 = new Focus(autd.Geometry.Center + Vector3d(0., 0., 150.));
        let g2 = new Null();

        let grouping (dev: Device) (tr: Transducer) =
            if (tr.Position.X < cx) then "focus" :> obj else "null" :> obj
        let g = (new Group(grouping)).Set("focus", g1).Set("null", g2);
        let m = new Sine(150u * Hz);

        (m, g) |> autd.Send;