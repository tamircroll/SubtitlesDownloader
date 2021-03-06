﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Xml;
using SubtitlesDownloader.Files;

namespace SubtitlesDownloader.OpenSubtitles
{

    public class SubtitleObject
    {
        public SubtitleObject(XmlNode i_XmlData, MovieFileInfo i_MovieFileInfo, MyFileInfo i_SrtFileInfo)
        {
            XmlData = i_XmlData;
            MovieFile = i_MovieFileInfo;
            ZipFile =
                new MyFileInfo(string.Format(@"{0}\{1}\tempFolder{2}\{2}.zip",
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), SetupData.SETUP_FOLDER_NAME,
                    i_MovieFileInfo.Hash));
            SrtFile = i_SrtFileInfo;
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
            try
            {
                DownloadZipped();
                UnZipp();
                CopyToFileFolder();
            }
            finally
            {
                if (Directory.Exists(ZipFile.getFolderPath()))
                    Directory.Delete(ZipFile.getFolderPath(), true);
            }
        }

        private void CopyToFileFolder()
        {
            string srtFile = new FilesUtiles().getAllFilesWithExtention(ZipFile.getFolderPath(), "srt").FirstOrDefault();
            if (srtFile != null) File.Copy(srtFile, SrtFile.FilePath, true);
        }

        public void DownloadZipped()
        {
            using (var client = new WebClient())
            {
                if (Directory.Exists(ZipFile.getFolderPath()))
                    Directory.Delete(ZipFile.getFolderPath(), true);

                Directory.CreateDirectory(ZipFile.getFolderPath());
                client.DownloadFile(LinkToDownload, ZipFile.FilePath);
            }
        }

        public void UnZipp()
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(ZipFile.FilePath, ZipFile.getFolderPath());
        }
    }
}