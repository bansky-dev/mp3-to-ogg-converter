using System;
using System.IO;
using NAudio.Wave;
using NVorbis;

namespace mp3_to_ogg_rework;

class Program
{
    static string mp3FilePath;
    static string pcmOutputPath;
    static bool isLooped = true;
    static void Main(string[] args)
    {
        while(isLooped){

            menuHandler();

            int choice = int.Parse(Console.ReadLine());

            if(choice == 1){
                System.Console.Write("Choose an MP3 file path (full path): ");
                mp3FilePath = Console.ReadLine();
                System.Console.Write("Choose an PCM file path (full path): ");
                pcmOutputPath = Console.ReadLine();
                
                decodeMp3ToPcm(mp3FilePath, pcmOutputPath);
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

    static void menuHandler(){
        Console.WriteLine("=====MP3 TO OGG CONVERTER=====");
        Console.WriteLine("Options:");
        Console.WriteLine("[1] Convert MP3 to OGG file");
        Console.WriteLine("[2] More options soon...");
        Console.Write("Choose:");

    }

    

}
