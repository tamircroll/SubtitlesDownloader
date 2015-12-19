using SubtitlesDownloader.Files;
using SubtitlesDownloader.OpenSubtitles;

namespace SubtitlesDownloader
{
    using System;
    using System.Collections.Generic;

    public class SubtitleDownload //TODO Therads
    {

        public bool Download(MovieFileInfo i_FileInfo, List<string> i_Languages)
        {
            if (FilesUtiles.FileExisits(i_FileInfo)) return true;

            foreach (string language in i_Languages)
            {
                bool downloaded = DownloadSubsInLanguage(i_FileInfo, language);
                if (downloaded) return true;
            }

            return false;
        }

        public bool DownloadSubsInLanguage(MovieFileInfo i_File, string i_Language)
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

        private bool tryDownloadAny(List<SubtitleInfo> i_FilteredSubs)
        {
            for (int i = 0; i < i_FilteredSubs.Count; i++)
            {
                try
                {
                    i_FilteredSubs[i].DownloadFile();
                }
                catch (Exception e)
                {
                    string continueMsg = (i + 1 < i_FilteredSubs.Count) ? "keep trying" : "No more tries";
                    Console.WriteLine("Error occurred in: {0}({1}). Error msg: {2}, {3}",
                        i_FilteredSubs[i].MovieFile.getFileName(), i_FilteredSubs[i].Languagh, e.Message, continueMsg);
                }

                if (FilesUtiles.FileExisits(i_FilteredSubs[i].SrtFile)) return true;
            }

            return false;
        }
    }
}
