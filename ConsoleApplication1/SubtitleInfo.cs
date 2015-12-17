using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication1.Files;

namespace ConsoleApplication1
{

    public class SubtitleInfo
    {
        private string token;
        private MovieFileInfo m_FileInfo;

        public SubtitleInfo(XmlNode i_XmlData, MovieFileInfo i_FileInfo, string i_Token)
        {
            XmlData = i_XmlData;
            token = i_Token;
            m_FileInfo = i_FileInfo;
            XmlNodeList xmlMembersList = i_XmlData.ChildNodes;
            Id = XmlUtiles.getMemberValueByName(xmlMembersList, "IDSubtitleFile");
            LinkToDownload = XmlUtiles.getMemberValueByName(xmlMembersList, "ZipDownloadLink");
            Encoding = XmlUtiles.getMemberValueByName(xmlMembersList, "SubEncoding");
            Languagh = XmlUtiles.getMemberValueByName(xmlMembersList, "LanguageName").ToLower();
        }



        public string LinkToDownload { get; private set; }

        public XmlNode XmlData { get; private set; }

        public string Id { get; private set; }

        public string Encoding { get; private set; }

        public string Languagh { get; private set; }

        public string GetEncodedSubs()
        {
            List<string> encodedSubs = new OpenSubtitlesDownloader().GetEncodedSubs(token, new List<string> { Id });
            return encodedSubs[0];
        }


        public string SubtitlePath()
        {
            return pathToFileWithoutExtention() + @".srt";
        }

        public string ZippedSubtitlePath()
        {
            return m_FileInfo.getFileFoler() + @"\tempFolder\" + m_FileInfo.getFileName() + @".zip";
        }

        public string ZippedFolderPath()
        {
            return m_FileInfo.getFileFoler() + @"\tempFolder\";
        }

        private string pathToFileWithoutExtention()
        {
            return m_FileInfo.getFileFoler() + @"\" + m_FileInfo.getFileName();
        }


        public string MovieFilePath()
        {
            return m_FileInfo.FilePath;
        }

        public void DownloadFile()
        {
            DownloadZipped();
            UnZipp();
            CopyToFileFolder();
            DeleteTempFolder();
        }

        private void DeleteTempFolder()
        {
            
        }

        private void CopyToFileFolder()
        {
            string srtFile = new FilesUtiles().getAllFilesWithExtention(ZippedFolderPath(), "srt").First();
            File.Copy(srtFile, m_FileInfo.PathToFileWithOutExtention() + ".srt", true);
            Directory.Delete(ZippedFolderPath(), true);
        }

        public void DownloadZipped()
        {
            using (var client = new WebClient())
            {
                Directory.CreateDirectory(m_FileInfo.getFileFoler() + @"\tempFolder\");
                client.DownloadFile(LinkToDownload, ZippedSubtitlePath());
            }
        }

        public void UnZipp()
        {
            ZipFile.ExtractToDirectory(ZippedSubtitlePath(), Path.GetDirectoryName(ZippedSubtitlePath()));
        }
    }
}
