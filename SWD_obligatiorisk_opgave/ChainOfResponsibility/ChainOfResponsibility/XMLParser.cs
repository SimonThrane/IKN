using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace ChainOfResponsibility
{
    class XMLParser<T> : ChainHandler<T>
    {
        public override T Parse(string inputdata, string inputtype)
        {
            if (inputtype.ToLower() == "xml")
            {
                Console.WriteLine("This is xml, I am parsing this");
                var serializer = new XmlSerializer(typeof(T));
                var type = (T) serializer.Deserialize(new StringReader(inputdata));
                return type;
            }
            return NextChain.Parse(inputdata, inputtype);
        }
    }
}