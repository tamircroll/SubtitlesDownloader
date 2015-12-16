namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SubtitleInfo
    {
        private string m_XmlData;
        private string m_File;
        private string m_Encoding;

        public string XmlData
        {
            get { return m_XmlData; }
            set { m_XmlData = value; }
        }

        public string File
        {
            get { return m_File; }
            set { m_File = value; }
        }

        public string Encoding
        {
            get { return m_Encoding; }
            set { m_Encoding = value; }
        }
    }
}
