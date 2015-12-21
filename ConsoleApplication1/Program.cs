using System;
using System.Diagnostics;
using System.Windows.Forms;
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
            DialogResult dialogResult = mainForm.ShowDialog();

            if (dialogResult == DialogResult.Cancel) return;

            setupData.SaveData();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            new SubtitlesDownloader().DownloadAll(setupData);

            sw.Stop();
            Console.WriteLine("{0}{0}Run Time: {1}", Environment.NewLine, sw.Elapsed);
        }
    }
}