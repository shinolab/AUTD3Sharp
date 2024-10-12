using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public interface IWindow
    {
        public uint WindowSize { get; }
        internal DynWindow Window();
    }

    public struct BlackMan : IWindow
    {
        public uint WindowSize { get; }

        public BlackMan(uint windowSize)
        {
            WindowSize = windowSize;
        }

        DynWindow IWindow.Window() { return DynWindow.Blackman; }
    }

    public struct Rectangular : IWindow
    {
        public uint WindowSize { get; }

        public Rectangular(uint windowSize)
        {
            WindowSize = windowSize;
        }

        DynWindow IWindow.Window() { return DynWindow.Rectangular; }
    }

    public class SincInterpolation
    {
        public SincInterpolation() : this(new BlackMan(32)) { }
        public SincInterpolation(IWindow window)
        {
            _window = window;
        }

        internal DynSincInterpolator DynResampler() => new DynSincInterpolator
        {
            window = _window.Window(),
            window_size = _window.WindowSize
        };

        private readonly IWindow _window;
    }
}
