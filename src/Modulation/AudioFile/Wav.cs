using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    /// <summary>
    /// Modulation constructed from wav file
    /// <remarks>The wav data is re-sampled to the sampling frequency of Modulation.</remarks>
    /// </summary>
    [Modulation]
    public sealed partial class Wav : Driver.Datagram.Modulation.Modulation
    {
        private readonly string _filename;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Path to wav file</param>
        public Wav(string filename)
        {
            _filename = filename;
            Config = SamplingConfiguration.FromFrequency(4000);
        }

        internal override ModulationPtr ModulationPtr()
        {
            var filenameBytes = System.Text.Encoding.UTF8.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = filenameBytes)
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationWav(fp, Config.Internal, LoopBehavior.Internal).Validate();
                }
            }
        }
    }
}
