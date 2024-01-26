#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    /// <summary>
    /// Modulation constructed from raw pcm data file
    /// <remarks>The wav data is re-sampled to the sampling frequency of Modulation.</remarks>
    /// </summary>
    public sealed class RawPCM : Driver.Datagram.ModulationWithSamplingConfig<RawPCM>
    {
        private readonly string _filename;
        private readonly uint _sampleRate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Path to raw pcm file</param>
        /// <param name="sampleRate">Sampling rate of raw pcm data</param>
        public RawPCM(string filename, uint sampleRate) : base(SamplingConfiguration.FromFrequency(4000))
        {
            _filename = filename;
            _sampleRate = sampleRate;
        }

        internal override ModulationPtr ModulationPtr()
        {
            var filenameBytes = System.Text.Encoding.ASCII.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = filenameBytes)
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationRawPCM(fp, _sampleRate, Config.Internal).Validate();
                }
            }
        }
    }
}
