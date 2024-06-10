using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = Controller.Builder([new AUTD3(Vector3.Zero)]).Open(TwinCAT.Builder());

SampleRunner.Run(autd);
