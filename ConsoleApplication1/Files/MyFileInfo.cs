using System.IO;

namespace SubtitlesDownloader.Files
{

    public class MyFileInfo
    {
        public MyFileInfo(string i_FilePath)
        {
            FilePath = i_FilePath;
        }

        public string FilePath { get; private set; }

        public string getFileFolder()
        {
            return Path.GetDirectoryName(FilePath);
        }

        public string getFileName()
        {
            return Path.GetFileNameWithoutExtension(FilePath);
        }

        public string PathToFileWithOutExtention()
        {
            return getFileFolder() + @"\" + getFileName();
        }

        public string GetDirectoryName()
        {
            return Path.GetDirectoryName(FilePath);
        }
    }
}
