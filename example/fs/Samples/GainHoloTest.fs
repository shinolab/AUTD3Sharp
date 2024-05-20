namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain.Holo
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open type AUTD3Sharp.Units

module GainHoloTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let m = new Sine (150u * Hz);

        let center = autd.Geometry.Center + Vector3d(0, 0, 150);
        let backend = new NalgebraBackend();
        let g = (new GSPAT<NalgebraBackend>(backend))
                    .AddFocus(center + 20.0 * Vector3d.UnitX, 5e3 * Pa)
                    .AddFocus(center - 20.0 * Vector3d.UnitX, 5e3 * Pa);

        (m, g) |> autd.Send;
