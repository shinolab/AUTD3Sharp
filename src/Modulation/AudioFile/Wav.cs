#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    /// <summary>
    /// Modulation constructed from wav file
    /// <remarks>The wav data is re-sampled to the sampling frequency of Modulation.</remarks>
    /// </summary>
    public sealed class Wav : Internal.ModulationWithSamplingConfig<Wav>
    {
        private readonly string _filename;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Path to wav file</param>
        public Wav(string filename)
        {
            _filename = filename;
        }

        internal override ModulationPtr ModulationPtr()
        {
            var filenameBytes = System.Text.Encoding.UTF8.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    var ptr = NativeMethodsModulationAudioFile.AUTDModulationWav(fp).Validate();
                    if (Config != null)
                        ptr = NativeMethodsModulationAudioFile.AUTDModulationRawPCMWithSamplingConfig(ptr, Config.Value.Internal);
                    return ptr;
                }
            }
        }
    }
}
