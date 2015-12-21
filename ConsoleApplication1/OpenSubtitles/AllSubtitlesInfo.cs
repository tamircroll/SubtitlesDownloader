using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SubtitlesDownloader.Files;

namespace SubtitlesDownloader.OpenSubtitles
{
    public class AllSubtitlesInfo
    {
        private MovieFileInfo m_MovieFileInfo;
        private List<SubtitleInfo> m_AllSubtitlesInfos = new List<SubtitleInfo>();
        private OpenSubtitlesDataFetcher m_Downloader;
        private MyFileInfo m_SrtFile;

        public AllSubtitlesInfo(OpenSubtitlesDataFetcher i_Downloader, MovieFileInfo i_MovieFileInfo, MyFileInfo i_SrtFile)
        {
            m_Downloader = i_Downloader;
            m_MovieFileInfo = i_MovieFileInfo;
            m_SrtFile = i_SrtFile;

            setAllSubsInfo();
        }

        public List<SubtitleInfo> getAll()
        {
            return m_AllSubtitlesInfos;
        }

        private void setAllSubsInfo()
        {
            string searchResult = m_Downloader.getSearchResult(m_MovieFileInfo.Hash, m_MovieFileInfo.Length.ToString());
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(searchResult);
            XmlNode xmlMainData = xmlDoc.GetElementsByTagName("data")[0];
            XmlNodeList subtitleXmlsList = xmlMainData.ChildNodes;

            foreach (XmlNode subtitleXml in subtitleXmlsList)
            {
                SubtitleInfo subtitleInfo = new SubtitleInfo(subtitleXml, m_MovieFileInfo, m_SrtFile);
                m_AllSubtitlesInfos.Add(subtitleInfo);
            }
        }

        public List<SubtitleInfo> getFilteredByLanguage(string i_Language)
        {
            return m_AllSubtitlesInfos.Where(info => info.Languagh.ToLower() == i_Language.ToLower()).ToList();
        }
    }
}
