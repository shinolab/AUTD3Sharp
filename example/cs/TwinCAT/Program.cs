
using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).Open(TwinCAT.Builder());

SampleRunner.Run(autd);
