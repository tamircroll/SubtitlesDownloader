using System;
using System.Collections.Generic;
using SubtitlesDownloader.Files;

namespace SubtitlesDownloader  
{
    public class Downloader
    {

        public void Download(string i_Folder)
        {
            List<string> languages = new List<string> {"Hebrew", "English"};

            List<string> allMovies = new FilesUtiles().getAllMoviefilesInFolder(i_Folder, true);

            foreach (string file in allMovies)
            {
                MovieFileInfo fileInfo = new MovieFileInfo(file);

                bool downloaded = new SubtitleDownload().Download(fileInfo, languages);
                if (downloaded) continue;

                Console.WriteLine("Failed To download Subtitles to: {0}", fileInfo.getFileName());
            }
        }

    }
}
