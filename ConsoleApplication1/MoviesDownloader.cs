using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
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
            List<Thread> threads = new List<Thread>();
//            bool shouldSignOut = false;

            foreach (string file in allMovies)
            {
                MovieFileInfo fileInfo = new MovieFileInfo(file);
                MyFileInfo srtFile = new MyFileInfo(string.Format(@"{0}.srt", fileInfo.PathToFileWithOutExtention()));

                if (FilesUtiles.FileExisits(srtFile)) continue;

                SubtitleDownload subtitleDownload = getDownloadCreator(srtFile, fileInfo, languages);

//                subtitleDownload.Download();

                Thread oThread = new Thread(subtitleDownload.Download);
                threads.Add(oThread);
                oThread.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

//            shouldSignOut = true;// TODO: add logic
//            if (shouldSignOut)
//            {
                OpenSubtitlesDownloader.SignOut();
//                shouldSignOut = false;
//            }
        }

        private SubtitleDownload getDownloadCreator(MyFileInfo i_SrtFile, MovieFileInfo i_FileInfo, List<string> i_Languages)
        {
            lock (this)
            {
                SubtitleDownload subtitleDownload = new SubtitleDownload();

                subtitleDownload.SrtFile = i_SrtFile;
                subtitleDownload.MovieFile = i_FileInfo;
                subtitleDownload.Languages = i_Languages;
                
                return subtitleDownload;
            }
        }
    }
}
