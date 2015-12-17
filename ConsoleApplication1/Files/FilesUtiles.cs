using System.IO;

namespace ConsoleApplication1.Files
{

    public class FilesUtiles
    {
        public void WriteToFile(string i_FileName, string i_ContentToWrite)
        {      
            StreamWriter file = new StreamWriter(i_FileName);
            file.WriteLine(i_ContentToWrite);
        }

        public string[] getAllFilesWithExtention(string i_FolderPath, string i_Extention)
        {
            return Directory.GetFiles(i_FolderPath, "*." + i_Extention, SearchOption.AllDirectories);
        }
    }
}
