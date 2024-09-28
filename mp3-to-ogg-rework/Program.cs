using System;
using System.IO;
using NAudio.Wave;
using NVorbis;

namespace mp3_to_ogg_rework;

class Program
{
    static string mp3FilePath;
    static string pcmFilePath;
    static bool isLooped = true;
    static void Main(string[] args)
    {
        while(isLooped){

            //TODO: Change everything from pcm to ogg

            menuHandler();

            int choice = int.Parse(Console.ReadLine());

            if(choice == 1){
                System.Console.Write("Choose an MP3 file path (full path): ");
                mp3FilePath = Console.ReadLine();
                System.Console.Write("Choose an PCM file path (full path): ");
                pcmFilePath = Console.ReadLine();
                
                decodeMp3ToPcm(mp3FilePath, pcmFilePath);
                encodePcmToOgg();
            }
            else if(choice == 2)
            {
                System.Console.WriteLine("It's still under construction sir!");
            }

        }
        
    }

    static void decodeMp3ToPcm(string mp3FilePath, string pcmOutputPath)
    {
        try{
            System.Console.WriteLine("Deocing MP3 to raw PCM file...");
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
        catch(Exception ex)
        {
            System.Console.WriteLine($"Error occured during decoding! {ex}");
        }
        
    }


    //TODO: Organize these datatypes and entire code;
    static void encodePcmToOgg(string pcmPath){
        var oggStream = new FileStream(oggOutputPath, FileMode.Create);
        var vorbisEncoder = new OggVorbisEncoder.OggEncoder();

        using (var pcmStream = new FileStream(pcmFilePath, FileMode.Open, FileAccess.Read))
        {
            var pcmWaveFormat = new WaveFormat(sampleRate, 16, channels);
            var waveProvider = new RawSourceWaveStream(pcmStream, pcmWaveFormat);

            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
            {
                // Encode the PCM data to OGG using the encoder
                vorbisEncoder.WriteSamples(buffer, bytesRead, channels, sampleRate);
            }

            // Finalize the encoding and close the stream
            vorbisEncoder.Close();
        }

        Console.WriteLine("PCM has been successfully encoded to OGG!");
    }

    static void menuHandler(){
        Console.WriteLine("=====MP3 TO OGG CONVERTER=====");
        Console.WriteLine("Options:");
        Console.WriteLine("[1] Convert MP3 to OGG file");
        Console.WriteLine("[2] More options soon...");
        Console.Write("Choose:");

    }

    

}
