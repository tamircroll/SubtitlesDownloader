using System;
using System.Diagnostics;
using SubtitlesDownloader.Forms;

namespace SubtitlesDownloader
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            SetupData setupData = new SetupData();

            MainForm mainForm = new MainForm(setupData);
            mainForm.ShowDialog();
            setupData.SaveData();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            new MoviesDownloader().DownloadAll(setupData.Path);

            sw.Stop();
            TimeSpan elapsedTime = sw.Elapsed;
            Console.WriteLine("{0}{0}Run Time: {1}", Environment.NewLine, elapsedTime);
         
            Console.ReadLine();
        }
    }
}