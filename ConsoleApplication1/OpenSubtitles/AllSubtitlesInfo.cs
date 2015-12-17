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
        private OpenSubtitlesDownloader m_Downloader;

        public AllSubtitlesInfo(OpenSubtitlesDownloader i_Downloader, MovieFileInfo i_MovieFileInfo)
        {
            m_Downloader = i_Downloader;
            m_MovieFileInfo = i_MovieFileInfo;
            setAllSubsInfo();
        }

        public List<SubtitleInfo> getAll()
        {
            return m_AllSubtitlesInfos;
        }

        private void setAllSubsInfo()
        {
            string token = m_Downloader.GetToken();
            string searchResult = m_Downloader.getSearchResult(m_MovieFileInfo.Hash, token, m_MovieFileInfo.Length.ToString());
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(searchResult);
            XmlNode xmlMainData = xmlDoc.GetElementsByTagName("data")[0];
            XmlNodeList subtitleXmlsList = xmlMainData.ChildNodes;

            foreach (XmlNode subtitleXml in subtitleXmlsList)
            {
                SubtitleInfo subtitleInfo = new SubtitleInfo(subtitleXml, m_MovieFileInfo);
                m_AllSubtitlesInfos.Add(subtitleInfo);
            }
        }

        public List<SubtitleInfo> getFilteredByLanguage(string i_Language)
        {
            if (m_AllSubtitlesInfos == null) setAllSubsInfo();
            return m_AllSubtitlesInfos.Where(info => info.Languagh.ToLower() == i_Language.ToLower()).ToList();
        }
    }
}
