using System.Xml;

namespace ConsoleApplication1
{
    public class XmlUtiles
    {
        public static string getMemberValueByName(XmlNodeList i_Members, string name)
        {

            foreach (XmlNode member in i_Members)
            {
                if (member.FirstChild.InnerText != name) continue;

                return member.ChildNodes[1].FirstChild.InnerText;
            }

            return null;
        }
    }
}
