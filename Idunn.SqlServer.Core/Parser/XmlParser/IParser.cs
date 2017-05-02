using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Core.Parser.XmlParser
{
    public interface IParser<T>
    {
        T Parse(XmlNode node);
    }
}
