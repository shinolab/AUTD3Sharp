using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(TwinCAT.Builder());

SampleRunner.Run(autd);
