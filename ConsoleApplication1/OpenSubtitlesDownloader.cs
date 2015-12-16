using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    public class OpenSubtitlesDownloader
    {
        private readonly string m_OpensubtitlesUrl = @"http://api.opensubtitles.org:80/xml-rpc";
        private readonly List<string> m_Languages = new List<string> { "hebrew", "english" };

        public string GetToken()
        {
            string responseFromServer = getPostResponse(m_OpensubtitlesUrl, getTokenXMLRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);

            XmlNode firstChild = xmlDoc.GetElementsByTagName("string")[0];

            return firstChild.InnerText;
        }

        public List<string> SearchSubs(string i_Hash, string i_Token, string i_FileLength)
        {
            Console.WriteLine("Hash: " + i_Hash + "Token: " + i_Token + "File Length: " + i_FileLength);
            string searchSubsXml = string.Format(serachSubsRequestXml, i_Token, i_Hash, i_FileLength);

            string response = getPostResponse(m_OpensubtitlesUrl, searchSubsXml);

            return getIdsFromXml(response);
        }




        private List<string> getIdsFromXml(string i_Response)
        {
            List<string> ids = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(i_Response);
            XmlNode allSubsData = xmlDoc.GetElementsByTagName("data")[0];
            XmlNodeList subsNodesLst = allSubsData.ChildNodes;

            

            foreach (XmlNode subsData in subsNodesLst)
            {
                if (!desiredLanguage(subsData.FirstChild)) continue;

                string id = extractId(subsData.FirstChild);
                if (id != null) ids.Add(id);
            }

            return ids;
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



        private string extractId(XmlNode i_Node)
        {
            XmlNodeList memberNodes = i_Node.ChildNodes;

            foreach (XmlNode member in memberNodes)
            {
                if (member.FirstChild.InnerText != "IDSubMovieFile") continue;

                return member.ChildNodes[1].FirstChild.InnerXml;
            }

            return null;
        }











        public void DownloadSubs(string i_Token, List<string> sutitlesIds)
        {
            string downloadSubsXml = string.Format(downloadXmlRequest, i_Token, getIdsAsXmlList(sutitlesIds));

            string response = getPostResponse(m_OpensubtitlesUrl, downloadSubsXml);

            Console.WriteLine(response);
        }

        private string getPostResponse(string i_Url, string i_BodyStr)
        {
            WebRequest request = WebRequest.Create(i_Url);
            byte[] body = Encoding.UTF8.GetBytes(i_BodyStr);

            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = body.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(body, 0, body.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse) response).StatusDescription);
            dataStream = response.GetResponseStream();
            if (dataStream != null)
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();

                return responseFromServer;
            }

            throw new Exception("Couldn't get info from server");
        }

        private string getIdsAsXmlList(List<string> sutitlesIds)
        {
            StringBuilder output = new StringBuilder();

            foreach (string id in sutitlesIds)
            {
                output.AppendLine("<value><int>" + id + "</int></value>");
            }

            return output.ToString();
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
      <value><int>1951894257</int></value>
      <value><int>1951853345</int></value>
     </data>
    </array>
   </value>
  </param>
 </params>
</methodCall>";

    }
}
