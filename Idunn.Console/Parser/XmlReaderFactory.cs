using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Idunn.Console.Parser.XmlParser
{
    public class XmlReaderFactory
    {
        public XmlReader Instantiate(StreamReader reader)
        {
            var xmlReader = XmlReader.Create(reader);
            return xmlReader;
        }
        
    }
}
