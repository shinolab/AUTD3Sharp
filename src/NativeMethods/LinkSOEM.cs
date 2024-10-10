// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Utils;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsLinkSOEM
    {
        const string __DllName = "autd3capi_link_soem";



        [DllImport(__DllName, EntryPoint = "AUTDAUTDLinkSOEMTracingInit", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDAUTDLinkSOEMTracingInit();

        [DllImport(__DllName, EntryPoint = "AUTDAdapterPointer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern EthernetAdaptersPtr AUTDAdapterPointer();

        [DllImport(__DllName, EntryPoint = "AUTDAdapterGetSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDAdapterGetSize(EthernetAdaptersPtr adapters);

        [DllImport(__DllName, EntryPoint = "AUTDAdapterGetAdapter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDAdapterGetAdapter(EthernetAdaptersPtr adapters, uint idx, byte* desc, byte* name);

        [DllImport(__DllName, EntryPoint = "AUTDAdapterPointerDelete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDAdapterPointerDelete(EthernetAdaptersPtr adapters);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEM", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEM();

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithSendCycle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithSendCycle(LinkSOEMBuilderPtr soem, ulong cycle);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithSync0Cycle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithSync0Cycle(LinkSOEMBuilderPtr soem, ulong cycle);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithBufSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithBufSize(LinkSOEMBuilderPtr soem, uint buf_size);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithTimerStrategy", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithTimerStrategy(LinkSOEMBuilderPtr soem, TimerStrategy timer_strategy);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithSyncMode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithSyncMode(LinkSOEMBuilderPtr soem, SyncMode mode);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithSyncTolerance", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithSyncTolerance(LinkSOEMBuilderPtr soem, ulong tolerance_ns);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithSyncTimeout", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithSyncTimeout(LinkSOEMBuilderPtr soem, ulong timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithIfname", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithIfname(LinkSOEMBuilderPtr soem, byte* ifname);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithStateCheckInterval", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithStateCheckInterval(LinkSOEMBuilderPtr soem, uint interval_ms);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMStatusGetMsg", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkSOEMStatusGetMsg(Status src, byte* dst);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithErrHandler", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithErrHandler(LinkSOEMBuilderPtr soem, ConstPtr handler, ConstPtr context);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMWithTimeout", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkSOEMBuilderPtr AUTDLinkSOEMWithTimeout(LinkSOEMBuilderPtr soem, ulong timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDLinkSOEMIntoBuilder", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkSOEMIntoBuilder(LinkSOEMBuilderPtr soem);

        [DllImport(__DllName, EntryPoint = "AUTDLinkRemoteSOEM", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultLinkRemoteSOEMBuilder AUTDLinkRemoteSOEM(byte* addr);

        [DllImport(__DllName, EntryPoint = "AUTDLinkRemoteSOEMWithTimeout", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkRemoteSOEMBuilderPtr AUTDLinkRemoteSOEMWithTimeout(LinkRemoteSOEMBuilderPtr soem, ulong timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDLinkRemoteSOEMIntoBuilder", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkRemoteSOEMIntoBuilder(LinkRemoteSOEMBuilderPtr soem);


    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct EthernetAdaptersPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LinkSOEMBuilderPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LinkRemoteSOEMBuilderPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultLinkRemoteSOEMBuilder
    {
        public LinkRemoteSOEMBuilderPtr result;
        public uint err_len;
        public ConstPtr err;
    }


    public enum Status : byte
    {
        Error = 0,
        StateChanged = 1,
        Lost = 2,
    }

    public enum TimerStrategy : byte
    {
        Sleep = 0,
        BusyWait = 1,
    }


}
    