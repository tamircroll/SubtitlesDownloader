using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    public class OpenSubtitlesDownloader
    {
        private readonly RestApi m_RestApi = new RestApi();
        private readonly string m_OpensubtitlesUrl = @"http://api.opensubtitles.org:80/xml-rpc";
        private readonly List<string> m_Languages = new List<string> {"heb", "eng"};

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
            string searchSubsXml = string.Format(serachSubsRequestXml, i_Token, getAllLanguagesCodesAsString(), i_Hash, i_FileLength);
            return m_RestApi.sendPostRequest(m_OpensubtitlesUrl, searchSubsXml);
        }

        private string getAllLanguagesCodesAsString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string language in m_Languages)
            {
                sb.Append(language);
            }

            return sb.ToString();
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
         <name>sublanguageid</name>
         <value><string>{1}</string>
         </value>
        </member>
        <member>
         <name>moviehash</name>
         <value><string>{2}</string></value>
        </member>
        <member>
         <name>moviebytesize</name>
         <value><double>{3}</double></value>
        </member>
       </struct>
      </value>
     </data>
    </array>
   </value>
  </param>
 </params>
</methodCall>";

    }
}