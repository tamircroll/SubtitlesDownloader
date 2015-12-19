using System;
using System.Diagnostics;

namespace SubtitlesDownloader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            new MoviesDownloader().DownloadAll(@"C:\Users\dell\Desktop\Movies");

            sw.Stop();
            TimeSpan elapsedTime = sw.Elapsed;
            Console.WriteLine("{0}{0}Run Time: {1}", Environment.NewLine, elapsedTime);
         
            Console.ReadLine();
        }
    }
}