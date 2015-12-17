using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using ConsoleApplication1.Files;
using ConsoleApplication1.OpenSubtitles;

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
            string file = @"C:\Users\dell\Desktop\Movies\Temp\Temp.avi";

            AllSubtitlesInfo subtitlesInfo = new AllSubtitlesInfo(new OpenSubtitlesDownloader(), new MovieFileInfo(file));
            List<SubtitleInfo> subtitleInfos = subtitlesInfo.getAll();
            List<SubtitleInfo> hebSubtitles = subtitlesInfo.getFilteredByLanguage("Hebrow");
            SubtitleInfo subtitleInfo = subtitleInfos[0];
            subtitleInfo.DownloadFile();
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