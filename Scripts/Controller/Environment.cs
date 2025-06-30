using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public sealed class Environment
    {
        internal readonly EnvironmentPtr _ptr;


        internal Environment(EnvironmentPtr ptr)
        {
            _ptr = ptr;
        }

        public float SoundSpeed
        {
            get => NativeMethodsBase.AUTDEnvironmentGetSoundSpeed(_ptr);
            set => NativeMethodsBase.AUTDEnvironmentSetSoundSpeed(_ptr, value);
        }
        public void SetSoundSpeedFromTemp(float temp, float k = 1.4f, float r = 8.31446261815324f, float m = 28.9647e-3f) => NativeMethodsBase.AUTDEnvironmentSetSoundSpeedFromTemp(_ptr, temp, k, r, m);

        public float Wavelength() => NativeMethodsBase.AUTDEnvironmentWavelength(_ptr);
        public float Wavenumber() => NativeMethodsBase.AUTDEnvironmentWavenumber(_ptr);
    }
}