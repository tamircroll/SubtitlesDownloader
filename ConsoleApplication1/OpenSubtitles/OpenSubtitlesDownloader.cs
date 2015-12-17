using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    public class OpenSubtitlesDownloader
    {
        private readonly RestApi m_RestApi = new RestApi();
        private readonly string m_OpensubtitlesUrl = @"http://api.opensubtitles.org:80/xml-rpc";
        private readonly List<string> m_Languages = new List<string>{};

        public OpenSubtitlesDownloader()
        {
        }

        public OpenSubtitlesDownloader(List<string> i_Languages)
        {
            m_Languages = i_Languages;
        }

        public string GetToken()
        {
            string responseFromServer = m_RestApi.sendPostRequest(m_OpensubtitlesUrl, getTokenXMLRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);

            XmlNode firstChild = xmlDoc.GetElementsByTagName("string")[0];

            return firstChild.InnerText;
        }

        public string getSearchResult(string i_Hash, string i_Token, string i_FileLength)
        {
            string searchSubsXml = string.Format(serachSubsRequestXml, i_Token, i_Hash, i_FileLength);
            return m_RestApi.sendPostRequest(m_OpensubtitlesUrl, searchSubsXml);
        }

        public List<string> GetEncodedSubs(string i_Token, List<string> sutitlesIds)
        {
            string downloadSubsXml = string.Format(downloadXmlRequest, i_Token, getIdsAsXmlList(sutitlesIds));
            string response = m_RestApi.sendPostRequest(m_OpensubtitlesUrl, downloadSubsXml);
            return extractAllDataByNodeName(response, "data", false);
        }

        private List<string> extractAllDataByNodeName(string i_Response, string i_DataToExtract, bool i_ValidateLanguagh)
        {
            List<string> allData = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(i_Response);
            XmlNode allSubsData = xmlDoc.GetElementsByTagName("data")[0];
            XmlNodeList allValueNodes = allSubsData.ChildNodes;

            foreach (XmlNode subsData in allValueNodes)
            {
                if (i_ValidateLanguagh && !desiredLanguage(subsData.FirstChild)) continue;

                string data = extractDataByNodeName(subsData.FirstChild, i_DataToExtract);
                if (data != null) allData.Add(data);
            }

            return allData;
        }


        private string extractDataByNodeName(XmlNode i_Node, string i_DataToExtract)
        {
            XmlNodeList memberNodes = i_Node.ChildNodes;

            foreach (XmlNode member in memberNodes)
            {
                if (member.FirstChild.InnerText != i_DataToExtract) continue;

                return member.ChildNodes[1].FirstChild.InnerText;
            }

            return null;
        }

        private string getIdsAsXmlList(List<string> sutitlesIds)
        {
            StringBuilder output = new StringBuilder();

            foreach (string id in sutitlesIds)
            {
                output.AppendLine(string.Format("<value><int>{0}</int></value>" , id));
            }

            return output.ToString();
        }

        private bool desiredLanguage(XmlNode i_Node)
        {
            XmlNodeList memberNodes = i_Node.ChildNodes;

            foreach (XmlNode member in memberNodes)
            {
                if (member.FirstChild.InnerText != "LanguageName") continue;

                string language = member.ChildNodes[1].FirstChild.InnerXml;
                if (m_Languages.Contains(language.ToLower())) return true;

                return false;
            }

            return false;
        }

        private readonly string getTokenXMLRequest =
 @"<methodCall>
 <methodName>LogIn</methodName>
 <params>
  <param>
   <value><string>tamircroll</string></value>
  </param>
  <param>
   <value><string>Asdf1234</string></value>
  </param>
  <param>
   <value><string>heb,eng</string></value>
  </param>
  <param>
   <value><string>SolEol 0.0.8</string></value>
  </param>
 </params>
</methodCall>";

        private readonly string serachSubsRequestXml =
@"<methodCall>
 <methodName>SearchSubtitles</methodName>
 <params>
  <param>
   <value><string>{0}</string></value>
  </param>
  <param>
   <value>
    <array>
     <data>
      <value>
       <struct>
        <member>
         <name>TheSubsDownloader</name>
         <value><string>heb,eng</string>
         </value>
        </member>
        <member>
         <name>moviehash</name>
         <value><string>{1}</string></value>
        </member>
        <member>
         <name>moviebytesize</name>
         <value><double>{2}</double></value>
        </member>
       </struct>
      </value>
     </data>
    </array>
   </value>
  </param>
 </params>
</methodCall>";

        private readonly string downloadXmlRequest =
@"<methodCall>
 <methodName>DownloadSubtitles</methodName>
 <params>
  <param>
   <value><string>{0}</string></value>
  </param>
  <param>
   <value>
    <array>
     <data>
      {1}
     </data>
    </array>
   </value>
  </param>
 </params>
</methodCall>";

    }
}



//        public List<string> SearchSubs(string i_Hash, string i_Token, string i_FileLength)
//        {
//            string searchSubsXml = string.Format(serachSubsRequestXml, i_Token, i_Hash, i_FileLength);
//            string response = m_RestApi.sendPostRequest(m_OpensubtitlesUrl, searchSubsXml);
//
//            return extractAllDataByNodeName(response, "IDSubtitleFile", true);
//        }

