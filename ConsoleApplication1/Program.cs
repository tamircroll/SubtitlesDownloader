using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

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
            string file = @"C:\Users\dell\Desktop\Movies\The Wire\Season 1\The Wire Season 1 Episode 01 - The Target.avi";
            byte[] moviehash = HashCoder.ComputeMovieHash( file);
            string hash = HashCoder.ToHexadecimal(moviehash);
            OpenSubtitlesDownloader openSubtitlesDownloader = new OpenSubtitlesDownloader();

            string token = openSubtitlesDownloader.getToken();
            openSubtitlesDownloader.searchSubs(hash, token, file);

            Console.ReadLine();
        }
    }
}