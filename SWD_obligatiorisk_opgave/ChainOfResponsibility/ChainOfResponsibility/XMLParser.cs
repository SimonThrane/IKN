using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ChainOfResponsibility
{
    class XMLParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            var serializer = new XmlSerializer(typeof(T));
            var type = (T)serializer.Deserialize(new StringReader(inputdata));
            return type;
        }
    }
}