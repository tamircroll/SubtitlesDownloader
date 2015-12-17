using System.IO;
using ConsoleApplication1.Files;
using FileInfo = ConsoleApplication1.Files.FileInfo;

namespace ConsoleApplication1
{

    public class MovieFileInfo : FileInfo
    {
        public MovieFileInfo(string i_FilePath) : base(i_FilePath)
        {
            byte[] moviehash = HashCoder.ComputeMovieHash(i_FilePath);
            Hash = HashCoder.ToHexadecimal(moviehash);
            Length = new System.IO.FileInfo(i_FilePath).Length;
        }

        public string Hash { get; private set; }
        
        public long Length { get; private set; }


    }
}
