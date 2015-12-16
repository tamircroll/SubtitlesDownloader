using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{

    public class MovieFileInfo
    {
        public MovieFileInfo(string i_Path)
        {
            Path = i_Path;
            byte[] moviehash = HashCoder.ComputeMovieHash(i_Path);
            Hash = HashCoder.ToHexadecimal(moviehash);
            Length = new FileInfo(i_Path).Length;
        }

        public string Hash { get; private set; }
        
        public string Path { get; private set; }

        public long Length { get; private set; }
    }
}
