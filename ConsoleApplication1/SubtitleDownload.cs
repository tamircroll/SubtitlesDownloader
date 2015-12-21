using System.Collections.Concurrent;
using SubtitlesDownloader.Files;
using SubtitlesDownloader.OpenSubtitles;

namespace SubtitlesDownloader
{
    using System;
    using System.Collections.Generic;

    public class SubtitleDownload
    {
        private AllSubtitlesInfo m_SubtitlesInfo;

        public List<string> Languages { get; set; }

        public MyFileInfo SrtFile { get; set; }

        public MovieFileInfo MovieFile { get; set; }

        public BlockingCollection<string> FailingMovies { get; set; }

        public void Download()
        {
            if (FilesUtiles.FileExisits(SrtFile)) return;
            bool succeed = false;

            if (!trySetSubtitlesInfo()) return;

            foreach (string language in Languages)
            {
                bool downloaded = DownloadSubsInLanguage(language);
                if (downloaded) return;
            }

            FailingMovies.Add(MovieFile.FilePath);
            Console.WriteLine("Failed To download Subtitles to: {0}", MovieFile.getFileName());
        }

        private bool DownloadSubsInLanguage(string i_Language)
        {
            List<SubtitleInfo> filteredSubs = m_SubtitlesInfo.getFilteredByLanguage(i_Language);

            if (filteredSubs.Count == 0)
            {
                Console.WriteLine("{0} - No Subs in language: {1} Exsists", MovieFile.getFileName(), i_Language);
                return false;
            }

            return tryDownloadAny(filteredSubs);
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
                    string continueMsg = (i + 1 < i_FilteredSubs.Count) ? "Keep trying" : "No more tries";
                    Console.WriteLine("Error occurred in: {0}({1}). Error msg: {2}, {3}",
                        i_FilteredSubs[i].MovieFile.getFileName(), i_FilteredSubs[i].Languagh, e.Message, continueMsg);
                }

                if (FilesUtiles.FileExisits(i_FilteredSubs[i].SrtFile)) return true;
            }

            return false;
        }

        private bool trySetSubtitlesInfo()
        {
            try
            {
                m_SubtitlesInfo = new AllSubtitlesInfo(new OpenSubtitlesDataFetcher(), MovieFile, SrtFile);
            }
            catch (Exception)
            {
                Console.WriteLine("Error occurred with file: {0}. Trying To re-signin", MovieFile.getFileName());
                return false;
            }

            return true;
        }
    }
}
