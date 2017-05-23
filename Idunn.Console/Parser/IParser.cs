using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.Console.Parser
{
    public interface IParser
    {
        object Parse(object node);
    }
}
