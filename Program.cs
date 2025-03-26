using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class Program
{
    public static void Main()
    {
        Console.Title = "FamilyFriendlyCreator | Made by https://github.com/ZygoteCode/";

        if (!Directory.Exists("inputs"))
        {
            Directory.CreateDirectory("inputs");
        }

        if (!Directory.Exists("outputs"))
        {
            Directory.CreateDirectory("outputs");
        }
        else
        {
            Directory.Delete("outputs", true);
            Directory.CreateDirectory("outputs");
        }

        foreach (string file in Directory.GetFiles("inputs"))
        {
            new Thread(() => RunFFMpeg($"-i \"{Path.GetFullPath(file)}\" -vf \"eq=saturation=" + File.ReadAllText("_saturation_value.txt") + $"\" \"{Path.GetFullPath("outputs")}\\{Path.GetFileName(file)}\"")).Start();
        }
    }

    private static void RunFFMpeg(string arguments)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg.exe",
            Arguments = $"-threads {Environment.ProcessorCount} {arguments}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        }).WaitForExit();
    }
}
