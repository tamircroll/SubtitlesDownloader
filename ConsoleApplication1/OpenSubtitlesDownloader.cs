using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    public class OpenSubtitlesDownloader
    {

        public string getToken()
        {
            WebRequest request = WebRequest.Create(@"http://api.opensubtitles.org:80/xml-rpc");
            byte[] body = Encoding.UTF8.GetBytes(getTokenXML);

            string responseFromServer = getResponseFromServer(request, body);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);

            XmlNode firstChild = xmlDoc.GetElementsByTagName("string")[0];

            return firstChild.InnerText;
        }

        public void searchSubs(string i_Hash, string i_Token, string i_File)
        {
            string fileLength = new FileInfo(i_File).Length.ToString();
            string searchSubsXml = string.Format(serachSubs, i_Token, i_Hash, fileLength);

            WebRequest request = WebRequest.Create(@"http://api.opensubtitles.org:80/xml-rpc");
            byte[] body = Encoding.UTF8.GetBytes(searchSubsXml);
            string response = getResponseFromServer(request, body);

            Console.WriteLine(response);
        }

        private string getResponseFromServer(WebRequest request, byte[] body)
        {
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = body.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(body, 0, body.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
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


        private string getTokenXML =
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
   <value><string>en</string></value>
  </param>
  <param>
   <value><string>SolEol 0.0.8</string></value>
  </param>
 </params>
</methodCall>";
        
        private readonly string serachSubs =
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
    }
}
