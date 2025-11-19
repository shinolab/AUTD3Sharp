using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Internal
{
    public sealed class Environment
    {
        internal readonly EnvironmentPtr Ptr;


        internal Environment(EnvironmentPtr ptr)
        {
            Ptr = ptr;
        }

        public float SoundSpeed
        {
            get => NativeMethodsBase.AUTDEnvironmentGetSoundSpeed(Ptr);
            set => NativeMethodsBase.AUTDEnvironmentSetSoundSpeed(Ptr, value);
        }
        public void SetSoundSpeedFromTemp(float temp, float k = 1.4f, float r = 8.31446261815324f, float m = 28.9647e-3f) => NativeMethodsBase.AUTDEnvironmentSetSoundSpeedFromTemp(Ptr, temp, k, r, m);

        public float Wavelength() => NativeMethodsBase.AUTDEnvironmentWavelength(Ptr);
        public float Wavenumber() => NativeMethodsBase.AUTDEnvironmentWavenumber(Ptr);
    }
}