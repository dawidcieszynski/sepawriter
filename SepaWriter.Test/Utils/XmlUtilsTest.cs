using System.Text;
using System.Xml;
using Xunit;
using Perrich.SepaWriter.Utils;

namespace Perrich.SepaWriter.Test.Utils
{
    public class XmlUtilsTest
    {
        [Fact]
        public void ShouldCreateXmlBicForAProvidedBic()
        {
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            var el = (XmlElement)xml.AppendChild(xml.CreateElement("Document"));

            XmlUtils.CreateBic(el, new SepaIbanData { Bic="01234567" });
            Assert.Equal("<FinInstnId><BIC>01234567</BIC></FinInstnId>", el.InnerXml);
        }

        [Fact]
        public void ShouldCreateXmlUnknownBicForAnUnknwonBic()
        {
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            var el = (XmlElement)xml.AppendChild(xml.CreateElement("Document"));

            XmlUtils.CreateBic(el, new SepaIbanData { UnknownBic = true});
            Assert.Equal("<FinInstnId><Othr><Id>NOTPROVIDED</Id></Othr></FinInstnId>", el.InnerXml);
        }
    }
}