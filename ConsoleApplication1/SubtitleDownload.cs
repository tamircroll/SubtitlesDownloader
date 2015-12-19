using SubtitlesDownloader.Files;
using SubtitlesDownloader.OpenSubtitles;

namespace SubtitlesDownloader
{
    using System;
    using System.Collections.Generic;

    public class SubtitleDownload //TODO Therads
    {
        private List<string> m_Languages;
        private MyFileInfo m_SrtFile;
        private MovieFileInfo m_MovieFile;

        public SubtitleDownload(MovieFileInfo i_MovieFile, List<string> i_Languages, MyFileInfo i_SrtFile)
        {
            m_Languages = i_Languages;
            m_SrtFile = i_SrtFile;
            m_MovieFile = i_MovieFile;
        }

        public bool Download(ref bool i_ShouldSignOut)
        {
            if (FilesUtiles.FileExisits(m_SrtFile)) return true;

            i_ShouldSignOut = true;

            foreach (string language in m_Languages)
            {
                bool downloaded = DownloadSubsInLanguage(language);
                if (downloaded) return true;
            }

            return false;
        }

        private bool DownloadSubsInLanguage(string i_Language)
        {
            AllSubtitlesInfo subtitlesInfo = new AllSubtitlesInfo(new OpenSubtitlesDownloader(), m_MovieFile, m_SrtFile);
            List<SubtitleInfo> filteredSubs = subtitlesInfo.getFilteredByLanguage(i_Language);

            if (filteredSubs.Count > 0)
            {
                Console.WriteLine("Movie: " + m_MovieFile.getFileName());
                return tryDownloadAny(filteredSubs);
            }
            
            Console.WriteLine("{0} No Subs in language: {1} Exsists", m_MovieFile.getFileName(), i_Language);

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
