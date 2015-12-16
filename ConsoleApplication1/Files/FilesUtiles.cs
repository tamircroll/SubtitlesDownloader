using System.IO;
using System.Text;
using System.IO.Compression;

namespace ConsoleApplication1.Files
{

    public class FilesUtiles
    {
        public void WriteToFile(string i_FileName, string i_ContentToWrite)
        {      
            StreamWriter file = new StreamWriter(i_FileName);
            file.WriteLine(i_ContentToWrite);
        }

        public void UnZipBytes(string i_OutputFile, byte[] inputBytes)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(inputBytes), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);

                    WriteToFile(i_OutputFile, Encoding.Default.GetString(memory.ToArray()));
                }
            }
        }
    }
}
