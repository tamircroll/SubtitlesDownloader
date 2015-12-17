using System.IO;

namespace ConsoleApplication1
{

    public class MovieFileInfo
    {
        public MovieFileInfo(string i_FilePath)
        {
            FilePath = i_FilePath;
            byte[] moviehash = HashCoder.ComputeMovieHash(i_FilePath);
            Hash = HashCoder.ToHexadecimal(moviehash);
            Length = new FileInfo(i_FilePath).Length;
        }

        public string Hash { get; private set; }
        
        public string FilePath { get; private set; }

        public long Length { get; private set; }

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
    }
}
