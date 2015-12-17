using System.IO;

namespace SubtitlesDownloader.Files
{

    public class MovieFileInfo : MyFileInfo
    {
        public MovieFileInfo(string i_FilePath) : base(i_FilePath)
        {
            byte[] moviehash = HashCoder.ComputeMovieHash(i_FilePath);
            Hash = HashCoder.ToHexadecimal(moviehash);
            Length = new FileInfo(i_FilePath).Length;
        }

        public string Hash { get; private set; }
        
        public long Length { get; private set; }


    }
}
