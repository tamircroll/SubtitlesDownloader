using System.IO;

namespace ConsoleApplication1.Files
{

    public class FileInfo
    {
        public FileInfo(string i_FilePath)
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
