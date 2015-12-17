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

        public string getFileFoler()
        {
            return Path.GetDirectoryName(FilePath);
        }

        public string getFileName()
        {
            return Path.GetFileNameWithoutExtension(FilePath);
        }

        public string PathToFileWithOutExtention()
        {
            return getFileFoler() + @"\" + getFileName();
        }

        public string GetDirectoryName()
        {
            return Path.GetDirectoryName(FilePath);
        }
    }
}
