using System.Xml;

namespace SubtitlesDownloader.OpenSubtitles
{
    public class OpenSubtitlesDataFetcher
    {
        private static string s_Token;

        private readonly RestApi.RestApi m_RestApi = new RestApi.RestApi();
        private static readonly string m_OpensubtitlesUrl = @"http://api.opensubtitles.org:80/xml-rpc";

        private void setToken()
        {
            string responseFromServer = new RestApi.RestApi().sendPostRequest(m_OpensubtitlesUrl, getTokenXMLRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);

            s_Token = xmlDoc.GetElementsByTagName("string")[0].InnerText;
        }

        public static void SignOut()
        {
            if (s_Token != null)
                new RestApi.RestApi().sendPostRequest(m_OpensubtitlesUrl, string.Format(SignOutXmlRequest, s_Token));
        }

        public string getSearchResult(string i_Hash, string i_FileLength)
        {
            if (s_Token == null) setToken();
            string searchSubsXml = string.Format(serachSubsRequestXml, s_Token, i_Hash, i_FileLength);
            return m_RestApi.sendPostRequest(m_OpensubtitlesUrl, searchSubsXml);
        }

        private static readonly string getTokenXMLRequest =
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
   <value><string>TheSubsDownloader</string></value>
  </param>
 </params>
</methodCall>";


        private static readonly string SignOutXmlRequest = 
@"<methodCall>
 <methodName>LogOut</methodName>
 <params>
  <param>
   <value><string>{0}</string></value>
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
         <value><string></string>
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

    }
}