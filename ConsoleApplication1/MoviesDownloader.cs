using System;
using System.Collections.Generic;
using SubtitlesDownloader.Files;
using SubtitlesDownloader.OpenSubtitles;

namespace SubtitlesDownloader  
{
    public class MoviesDownloader
    {
        bool subFolders = true;

        public void DownloadAll(string i_Folder)
        {
            List<string> languages = new List<string> {"Hebrew", "English"};
            List<string> allMovies = new FilesUtiles().getAllMoviefilesInFolder(i_Folder, subFolders);
            bool shouldSignOut = false;

            foreach (string file in allMovies)
            {
                MovieFileInfo fileInfo = new MovieFileInfo(file);
                MyFileInfo srtFile = new MyFileInfo(string.Format(@"{0}.srt", fileInfo.PathToFileWithOutExtention()));

                if (FilesUtiles.FileExisits(srtFile)) continue;

                SubtitleDownload subtitleDownload = new SubtitleDownload(fileInfo, languages, srtFile);
                bool downloaded = subtitleDownload.Download(ref shouldSignOut);
                
                if (!downloaded) Console.WriteLine("Failed To download Subtitles to: {0}", fileInfo.getFileName());
            }


            
            shouldSignOut = true;// TODO: add logic
            if (shouldSignOut)
            {
                OpenSubtitlesDownloader.SignOut();
                shouldSignOut = false;
            }
        }
    }
}
