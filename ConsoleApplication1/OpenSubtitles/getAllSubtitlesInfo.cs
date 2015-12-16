using System.Xml;

namespace ConsoleApplication1.OpenSubtitles
{
    using System.Collections.Generic;

    public class getAllSubtitlesInfo
    {
        private MovieFileInfo m_FileInfo ;
        private List<SubtitleInfo> m_AllSubtitlesInfos;
        private OpenSubtitlesDownloader m_Downloader;

        public getAllSubtitlesInfo(OpenSubtitlesDownloader i_Downloader, MovieFileInfo i_FileInfo)
        {
            m_Downloader = i_Downloader;
            m_FileInfo = i_FileInfo;
            m_AllSubtitlesInfos = new List<SubtitleInfo>();
        }

        public List<SubtitleInfo> get()
        {
            string token = m_Downloader.GetToken();
            string searchResult = m_Downloader.getSearchResult(m_FileInfo.Hash, token, m_FileInfo.Length.ToString());
            var xmlDoc = new XmlDocument(); 
            xmlDoc.LoadXml(searchResult);
            XmlNode xmlMainData = xmlDoc.GetElementsByTagName("data")[0];
            XmlNodeList subtitleXmlsList = xmlMainData.FirstChild.ChildNodes;

            foreach (XmlNode subtitleXml in subtitleXmlsList)
            {
                SubtitleInfo subtitleInfo = new SubtitleInfo(subtitleXml, m_FileInfo, token);
                m_AllSubtitlesInfos.Add(subtitleInfo);
            }

            return null;
        }
    }
}
