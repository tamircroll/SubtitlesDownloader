using System.IO;
using System.Text.RegularExpressions;

namespace SubtitlesDownloader.Files
{

    public class MyFileInfo
    {
        public MyFileInfo(string i_FilePath)
        {
            FilePath = i_FilePath;
        }

        public string FilePath { get; set; }

        public string getFolderPath()
        {
            return Path.GetDirectoryName(FilePath);
        }

        public string getFileName()
        {
            return Path.GetFileNameWithoutExtension(FilePath);
        }

        public string PathToFileWithOutExtention()
        {
            return getFolderPath() + @"\" + getFileName();
        }

        public string GetDirectoryName()
        {
            string folderSeperator = @"\\";
            string[] foldersPath = Regex.Split(FilePath, folderSeperator);
            return foldersPath[foldersPath.Length - 2];
        }
    }
}
