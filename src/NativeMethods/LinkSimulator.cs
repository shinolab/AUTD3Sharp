// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Utils;
using AUTD3Sharp.Link;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsLinkSimulator
    {
        const string __DllName = "autd3capi_link_simulator";



        [DllImport(__DllName, EntryPoint = "AUTDLinkSimulatorTracingInit", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkSimulatorTracingInit();

        [DllImport(__DllName, EntryPoint = "AUTDLinkSimulatorTracingInitWithFile", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultStatus AUTDLinkSimulatorTracingInitWithFile(byte* path);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSimulator", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultLinkBuilder AUTDLinkSimulator(byte* addr);


    }



}
