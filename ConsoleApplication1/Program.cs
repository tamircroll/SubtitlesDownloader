using System;
using System.Collections.Generic;
using ConsoleApplication1.Files;
using ConsoleApplication1.OpenSubtitles;

namespace ConsoleApplication1
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            string Folder = @"C:\Users\dell\Desktop\Movies";
            List<string> languages = new List<string> {"Hebrew", "English"};

            List<string> allMovies = new FilesUtiles().getAllMoviefilesInFolder(Folder, true);

            foreach (string file in allMovies)
            {
                MovieFileInfo fileInfo = new MovieFileInfo(file);
                bool downloaded = tryDownload(fileInfo, "Hebrew");
                if (downloaded) continue;

                downloaded = tryDownload(fileInfo, "English");
                if (downloaded) continue;

                Console.WriteLine("Failed To download Subtitles to: {0}", fileInfo.getFileName());
            }

            Console.ReadLine();
        }

        private static bool tryDownload(MovieFileInfo i_FileInfo, string i_Language)
        {
            try
            {
                if (!FilesUtiles.FileExisits(i_FileInfo))
                {
                    return DownloadSubsToFile(i_FileInfo, i_Language);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in: {0}({1}). Error msg: {2}", i_FileInfo.getFileName(), i_Language,
                    e.Message);
                return false;
            }
        }

        public static bool DownloadSubsToFile(MovieFileInfo i_File, string i_Language)
        {
            AllSubtitlesInfo subtitlesInfo = new AllSubtitlesInfo(new OpenSubtitlesDownloader(), i_File);
            List<SubtitleInfo> filteredSubs = subtitlesInfo.getFilteredByLanguage(i_Language);

            if (filteredSubs.Count > 0)
            {
                return tryDownloadAny(filteredSubs);
            }

            Console.WriteLine("{0} No Subs in language: {1} Exsists", i_File.getFileName(), i_Language);
            return false;
        }

        private static bool tryDownloadAny(List<SubtitleInfo> filteredSubs)
        {
            for (int i = 0; i < filteredSubs.Count; i++)
            {
                try
                {
                    filteredSubs[i].DownloadFile();
                }
                catch (Exception e)
                {
                    string continueMsg = (i + 1 < filteredSubs.Count) ? "keep trying" : "No more tries";
                    Console.WriteLine("Error occurred in: {0}({1}). Error msg: {2}, {3}",
                        filteredSubs[i].MovieFile.getFileName(), filteredSubs[i].Languagh, e.Message, continueMsg);
                }
                if (FilesUtiles.FileExisits(filteredSubs[i].SrtFile)) return true;
            }

            return false;
        }
    }
}