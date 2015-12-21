using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using SubtitlesDownloader.Files;
using SubtitlesDownloader.OpenSubtitles;

namespace SubtitlesDownloader  
{
    public class SubtitlesDownloader
    {
        private readonly BlockingCollection<string> m_FailingMovies = new BlockingCollection<string>();

        public void DownloadAll(SetupData i_SetupData)
        {
            do
            {
                downloadSubtitlesToAllMovies(i_SetupData);
                if (i_SetupData.BackgroundRun == false) break;
                Thread.Sleep(300000);  // Every 5 minutes
            } while (true);

            OpenSubtitlesDataFetcher.SignOut();
        }

        private void downloadSubtitlesToAllMovies(SetupData i_SetupData)
        {
            List<string> allMovies = new FilesUtiles().getAllMoviefilesInFolder(i_SetupData.Path, i_SetupData.NoSubFolders);

            removeFailingMovies(allMovies);

            List<Thread> threads = new List<Thread>();

            foreach (string file in allMovies)
            {
                MovieFileInfo fileInfo = new MovieFileInfo(file);
                MyFileInfo srtFile = new MyFileInfo(string.Format(@"{0}.srt", fileInfo.PathToFileWithOutExtention()));

                if (FilesUtiles.FileExisits(srtFile)) continue;

                SubtitleDownload subtitleDownload = getInitSubtitleDownload(srtFile, fileInfo, i_SetupData.Languages);

                Thread oThread = new Thread(subtitleDownload.Download);
                threads.Add(oThread);
                oThread.Start();

                Thread.Sleep(500);
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        private void removeFailingMovies(List<string> allMovies)
        {
            foreach (string movie in m_FailingMovies)
            {
                if (allMovies.Contains(movie)) allMovies.Remove(movie);
            }
        }

        private SubtitleDownload getInitSubtitleDownload(MyFileInfo i_SrtFile, MovieFileInfo i_FileInfo, List<string> i_Languages)
        {
            lock (this)
            {
                SubtitleDownload subtitleDownload = new SubtitleDownload
                {
                    SrtFile = i_SrtFile,
                    MovieFile = i_FileInfo,
                    Languages = i_Languages,
                    FailingMovies = m_FailingMovies
                };

                return subtitleDownload;
            }
        }
    }
}
