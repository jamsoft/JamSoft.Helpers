using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JamSoft.Helpers.Serialization;
using Xunit;

namespace JamSoft.Helpers.Tests
{
    public class Utf8StringWriterTests
    {
        [Serializable]
        public class TestObject
        {
            public string SomeProperty { get; set; }
        }

        [Fact]
        public void Writes_Uppercase_Utf_Enccoding()
        {
            var xsSubmit = new XmlSerializer(typeof(TestObject));
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = false,
                Indent = true,
                Encoding = Encoding.UTF8
            };

            string xml;
            using (var sw = new UppercaseUtf8StringWriter())
            {
                xsSubmit.Serialize(sw, new TestObject { SomeProperty = "SomeValue" });
                xml = sw.ToString();
            }

            Assert.Contains("UTF", xml);
		}
    }
}
