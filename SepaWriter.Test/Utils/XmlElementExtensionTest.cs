using System.Text;
using System.Xml;
using Xunit;
using Perrich.SepaWriter.Utils;

namespace Perrich.SepaWriter.Test.Utils
{
    public class XmlElementExtensionTest
    {
        private const string name = "sample";
        private const string name2 = "sample2";
        private const string name3 = "sample3";
        private const decimal value = 12.5m;

        public XmlElement Prepare()
        {
            var document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            return (XmlElement) document.AppendChild(document.CreateElement("Document"));
        }

        [Fact]
        public void ShouldAddMultipleOrderedNewElement()
        {
            var element = Prepare();
            var el = element.NewElement(name);
            var el2 = element.NewElement(name2);
            var el3 = element.NewElement(name3);
            Assert.True(element.HasChildNodes);
            Assert.Equal(3, element.ChildNodes.Count);
            Assert.Equal(el, element.FirstChild);
            Assert.Equal(el2, element.ChildNodes[1]);
            Assert.Equal(el3, element.LastChild);
        }

        [Fact]
        public void ShouldAddNewElementWithAValue()
        {
            var element = Prepare();
            var el = element.NewElement(name, value);
            Assert.Equal(name, el.Name);
            Assert.Equal(value.ToString(), el.InnerText);
            Assert.True(element.HasChildNodes);
            Assert.Equal(1, element.ChildNodes.Count);
        }

        [Fact]
        public void ShouldAddNewElementWithoutValue()
        {
            var element = Prepare();
            var el = element.NewElement(name);
            Assert.Equal(name, el.Name);
            Assert.Empty(el.InnerText);
            Assert.True(element.HasChildNodes);
            Assert.Equal(1, element.ChildNodes.Count);
        }

        [Fact]
        public void ShouldAddNewElementExplicitlyWithoutValue()
        {
            var element = Prepare();
            var el = element.NewElement(name, null);
            Assert.Equal(name, el.Name);
            Assert.Empty(el.InnerText);
            Assert.True(element.HasChildNodes);
            Assert.Equal(1, element.ChildNodes.Count);
        }
    }
}