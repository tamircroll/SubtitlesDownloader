﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using SubtitlesDownloader.Files;

namespace SubtitlesDownloader.OpenSubtitles
{

    public class SubtitleInfo
    {
        public SubtitleInfo(XmlNode i_XmlData, MovieFileInfo i_MovieFileInfo)
        {
            XmlData = i_XmlData;
            MovieFile = i_MovieFileInfo;
            ZipFile = new MyFileInfo(MovieFile.getFileFoler() + @"\tempFolder\" + MovieFile.getFileName() + @".zip");
            SrtFile = new MyFileInfo(MovieFile.PathToFileWithOutExtention() + @".srt");
            XmlNodeList xmlMembersList = XmlData.FirstChild.ChildNodes;
            Id = XmlUtiles.getMemberValueByName(xmlMembersList, "IDSubtitleFile");
            LinkToDownload = XmlUtiles.getMemberValueByName(xmlMembersList, "ZipDownloadLink");
            Encoding = XmlUtiles.getMemberValueByName(xmlMembersList, "SubEncoding");
            Languagh = XmlUtiles.getMemberValueByName(xmlMembersList, "LanguageName").ToLower();
        }

        public MovieFileInfo MovieFile { get; set; }

        public MyFileInfo ZipFile { get; set; }

        public MyFileInfo SrtFile { get; set; }

        public string LinkToDownload { get; set; }

        public XmlNode XmlData { get; set; }

        public string Id { get; private set; }

        public string Encoding { get; private set; }

        public string Languagh { get; private set; }

        public string MovieFilePath()
        {
            return MovieFile.FilePath;
        }

        public void DownloadFile()
        {
            DownloadZipped();
            UnZipp();
            CopyToFileFolder();
            Console.WriteLine("Downloading {0} subtitles to: {1}", Languagh, MovieFile.getFileName());
            Directory.Delete(ZipFile.GetDirectoryName(), true);
        }

        private void CopyToFileFolder()
        {
            string srtFile = new FilesUtiles().getAllFilesWithExtention(ZipFile.GetDirectoryName(), "srt").First();
            File.Copy(srtFile, SrtFile.FilePath, true);
        }

        public void DownloadZipped()
        {
            using (var client = new WebClient())
            {
                Directory.CreateDirectory(ZipFile.GetDirectoryName());
                client.DownloadFile(LinkToDownload, ZipFile.FilePath);
            }
        }

        public void UnZipp()
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(ZipFile.FilePath, ZipFile.GetDirectoryName());
        }
    }
}