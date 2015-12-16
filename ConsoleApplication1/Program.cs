using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using ConsoleApplication1.Files;

namespace ConsoleApplication1
{
    internal class Program
    {
        private List<String> videoExtentions = new List<String>
        {
            "*.3g2","*.3gp","*.3gp2","*.3gpp","*.60d","*.ajp","*.asf","*.asx","*.avchd","*.avi","*.bik","*.bix","*.box","*.cam","*.dat","*.divx","*.dmf",
            "*.dv","*.dvr-ms","*.evo","*.flc","*.fli","*.flic","*.flv","*.flx","*.gvi","*.gvp","*.h264","*.m1v","*.m2p","*.m2ts","*.m2v","*.m4e","*.m4v",
            "*.mjp","*.mjpeg","*.mjpg","*.mkv","*.moov","*.mov","*.movhd","*.movie","*.movx","*.mp4","*.mpe","*.mpeg","*.mpg","*.mpv","*.mpv2","*.mxf","*.nsv",
            "*.nut","*.ogg","*.ogm","*.omf","*.ps","*.qt","*.ram","*.rm","*.rmvb","*.swf", "*.ts","*.vfw","*.vid","*.video","*.viv","*.vivo","*.vob","*.vro",
            "*.wm","*.wmv","*.wmx","*.wrap","*.wvx","*.wx","*.x264","*.xvid"
        };

        private static void Main(string[] args)
        {
            string file = @"C:\Users\dell\Desktop\Movies\Total Recall 1990\Total.Recall.Mind.Bending.mp4";
            byte[] moviehash = HashCoder.ComputeMovieHash( file);
            string hash = HashCoder.ToHexadecimal(moviehash);
            OpenSubtitlesDownloader openSubtitlesDownloader = new OpenSubtitlesDownloader(new List<string>{"hebrew"});
            string fileLength = new FileInfo(file).Length.ToString();
            string token = openSubtitlesDownloader.GetToken();
            List<string> ids = openSubtitlesDownloader.SearchSubs(hash, token, fileLength);
            List<string> encodedSubs = openSubtitlesDownloader.GetEncodedSubs(token, ids);
            FilesUtiles filesUtiles = new FilesUtiles();

            List<byte[]> decoded = getListDecoded(encodedSubs);

            filesUtiles.UnZipBytes(@"C:\Users\dell\Desktop\Movies\Total Recall 1990\subs.srt", decoded[0]);
        }

        public static List<byte[]> getListDecoded(List<string> i_StringEncodedLst)
        {
            List<byte[]> lst = new List<byte[]>();
            for (int i = 0; i < i_StringEncodedLst.Count; i++)
            {
                lst.Add(Decode(i_StringEncodedLst[i]));
            }

            return lst;
        }

        private static byte[] Decode(string i_EncodedString)
        {
            return Convert.FromBase64String(i_EncodedString);
        }
    }
}