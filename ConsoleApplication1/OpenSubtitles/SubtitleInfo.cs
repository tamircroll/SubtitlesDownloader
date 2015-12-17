using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Xml;
using ConsoleApplication1.Files;

namespace ConsoleApplication1.OpenSubtitles
{

    public class SubtitleInfo
    {
        private MovieFileInfo m_MovieFileInfo;
        private readonly MyFileInfo m_ZipFile;

        public SubtitleInfo(XmlNode i_XmlData, MovieFileInfo i_MovieFileInfo)
        {
            XmlData = i_XmlData;
            m_MovieFileInfo = i_MovieFileInfo;
            m_ZipFile = new Files.MyFileInfo(m_MovieFileInfo.getFileFoler() + @"\tempFolder\" + m_MovieFileInfo.getFileName() + @".zip");
            SrtFile = new Files.MyFileInfo(m_MovieFileInfo.PathToFileWithOutExtention() + @".srt");
            XmlNodeList xmlMembersList = i_XmlData.ChildNodes;
            Id = XmlUtiles.getMemberValueByName(xmlMembersList, "IDSubtitleFile");
            LinkToDownload = XmlUtiles.getMemberValueByName(xmlMembersList, "ZipDownloadLink");
            Encoding = XmlUtiles.getMemberValueByName(xmlMembersList, "SubEncoding");
            Languagh = XmlUtiles.getMemberValueByName(xmlMembersList, "LanguageName").ToLower();
        }

        public MyFileInfo SrtFile { get; private set; }

        public string LinkToDownload { get; private set; }

        public XmlNode XmlData { get; private set; }

        public string Id { get; private set; }

        public string Encoding { get; private set; }

        public string Languagh { get; private set; }

        public string MovieFilePath()
        {
            return m_MovieFileInfo.FilePath;
        }

        public void DownloadFile()
        {
            Console.WriteLine("Downloading subtitles to: " + m_MovieFileInfo.getFileName());
            DownloadZipped();
            UnZipp();
            CopyToFileFolder();
            Directory.Delete(m_ZipFile.GetDirectoryName(), true);
        }

        private void CopyToFileFolder()
        {
            string srtFile = new FilesUtiles().getAllFilesWithExtention(m_ZipFile.GetDirectoryName(), "srt").First();
            File.Copy(srtFile, SrtFile.FilePath, true);
        }

        public void DownloadZipped()
        {
            using (var client = new WebClient())
            {
                Directory.CreateDirectory(m_ZipFile.GetDirectoryName());
                client.DownloadFile(LinkToDownload, m_ZipFile.FilePath);
            }
        }

        public void UnZipp()
        {
            ZipFile.ExtractToDirectory(m_ZipFile.FilePath, m_ZipFile.GetDirectoryName());
        }
    }
}