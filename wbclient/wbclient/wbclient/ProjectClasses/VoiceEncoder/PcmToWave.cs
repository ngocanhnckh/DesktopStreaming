using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace VoiceEncoder
{
    public class PcmToWave
    {

        public void SavePcmToWav(MemoryStream Data, Stream Output, int BitPerSample, int SamplePeerSecond, int Channels)
        {

            BinaryWriter _output = new BinaryWriter(Output);
            // WAV header
            _output.Write("RIFF".ToCharArray());
            // RIFF chunk
            _output.Write((uint)(Data.Length));
            // Total Length Of Package To Follow
            _output.Write("WAVE".ToCharArray());
            // WAV chunk
            _output.Write("fmt ".ToCharArray());
            // FORMAT chunk
            _output.Write((uint)0x10);
            // Length Of FORMAT Chunk (Binary, always 0x10)
            _output.Write((ushort)0x1);
            // Always 0x01
            // Channel Numbers (Always 0x01=Mono, 0x02=Stereo)
            _output.Write((ushort)Channels);
            _output.Write((uint)SamplePeerSecond);
            // Sample Rate (Binary, in Hz)
            _output.Write((uint)(BitPerSample * SamplePeerSecond * Channels / 8));
            // Bytes Per Second
            // Bytes Per Sample: 1=8 bit Mono, 2=8 bit Stereo or 16 bit Mono, 4=16 bit Stereo
            _output.Write((ushort)(BitPerSample * Channels / 8));
            _output.Write((ushort)BitPerSample);
            // Bits Per Sample
            _output.Write("data".ToCharArray());
            _output.Write(Data.ToArray(), 0, (int)Data.Length);
        }
    }
}
