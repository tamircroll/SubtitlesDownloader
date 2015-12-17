namespace ConsoleApplication1.Files
{

    public class MovieFileInfo : Files.MyFileInfo
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
