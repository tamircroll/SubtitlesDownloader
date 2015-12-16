using System.Xml;

namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SubtitleInfo
    {
        private string token;

        public SubtitleInfo(XmlNode i_XmlData, MovieFileInfo i_FileInfo, string i_Token)
        {
            XmlData = i_XmlData;
            token = i_Token;
            XmlNodeList xmlMembersList = i_XmlData.ChildNodes;
            Encoding = XmlUtiles.getMemberValueByName(xmlMembersList, "SubEncoding");
            Id = XmlUtiles.getMemberValueByName(xmlMembersList, "IDSubtitleFile");
            Languagh = XmlUtiles.getMemberValueByName(xmlMembersList, "LanguageName");
        }

        public XmlNode XmlData { get; private set; }

        public string Id { get; private set; }

        public string Encoding { get; private set; }

        public string Languagh { get; private set; }

        public string GetEncodedSubs()
        {
            List<string> encodedSubs = new OpenSubtitlesDownloader().GetEncodedSubs(token, new List<string> { Id });
            return encodedSubs[0];
        }
    }
}
