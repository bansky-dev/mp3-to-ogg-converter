using System;
using System.IO;
using NAudio.Wave;
using NVorbis;


namespace mp3_to_ogg_rework;

class Program
{
    static void Main(string[] args)
    {
        string mp3FilePath = "input.mp3";  // Path to your MP3 file
        string pcmOutputPath = "output.pcm";  // Path for the PCM output file

        DecodeMp3ToPcm(mp3FilePath, pcmOutputPath);

    }

    static void DecodeMp3ToPcm(string mp3FilePath, string pcmOutputPath)
    {
        using (var mp3Reader = new Mp3FileReader(mp3FilePath))
        using (var pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader))
        using (var outputStream = new FileStream(pcmOutputPath, FileMode.Create, FileAccess.Write))
        {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = pcmStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
            }
        }

        Console.WriteLine("MP3 has been successfully decoded to PCM!");
    }

}
