using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SubtitlesDownloader.Files
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

        public static bool FileExisits(MyFileInfo i_File)
        {
            return File.Exists(string.Format("{0}.srt", i_File.PathToFileWithOutExtention()));
        }

        public List<string> getAllMoviefilesInFolder(string i_FolderPath, bool noSubFolders)
        {
            SearchOption allDirectories = noSubFolders ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            string[] allFilesArr = Directory.GetFiles(i_FolderPath, "*.*", allDirectories);

            return allFilesArr.Where(file => videoExtentions.Contains(Path.GetExtension(file).ToLower())).ToList();
        }

        private List<String> videoExtentions = new List<String>
        {
            ".3g2", ".3gp", ".3gp2", ".3gpp", ".60d", ".ajp", ".asf", ".asx", ".avchd", ".avi", ".bik", ".bix", ".box", ".cam", ".dat", ".divx", ".dmf", 
            ".dv", ".dvr-ms", ".evo", ".flc", ".fli", ".flic", ".flv", ".flx", ".gvi", ".gvp", ".h264", ".m1v", ".m2p", ".m2ts", ".m2v", ".m4e", ".m4v", 
            ".mjp", ".mjpeg", ".mjpg", ".mkv", ".moov", ".mov", ".movhd", ".movie", ".movx", ".mp4", ".mpe", ".mpeg", ".mpg", ".mpv", ".mpv2", ".mxf", ".nsv", 
            ".nut", ".ogg", ".ogm", ".omf", ".ps", ".qt", ".ram", ".rm", ".rmvb", ".swf",  ".ts", ".vfw", ".vid", ".video", ".viv", ".vivo", ".vob", ".vro", 
            ".wm", ".wmv", ".wmx", ".wrap", ".wvx", ".wx", ".x264", ".xvid"
        };
    }
}
