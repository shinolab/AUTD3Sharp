namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Utils
open AUTD3Sharp.Modulation

module CustomTest =
    type Focus (point: Vector3d) =
        inherit Gain()
        let Calc_ (dev: Device) (tr:Transducer) = 
            let mutable drive = new Drive();
            let dist = (tr.Position - point).L2Norm;
            drive.Phase <- Phase.FromRad(dist * tr.Wavenumber(dev.SoundSpeed));
            drive.Intensity <- EmitIntensity.Max;
            drive
        override this.Calc (geometry: Geometry) = AUTD3Sharp.Gain.Gain.Transform(geometry, Calc_);
        
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Disable()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        let m = new Sine 150;
        let g = new Focus (autd.Geometry.Center + Vector3d(0, 0, 150))

        (m, g) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
