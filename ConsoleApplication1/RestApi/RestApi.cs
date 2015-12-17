using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplication1.RestApi
{
    public class RestApi
    {
        public string sendPostRequest(string i_Url, string i_BodyStr)
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
    }
}