using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Console.Parser
{
    public interface IRootParser
    {
        IEnumerable<object> Parse(Stream stream);
    }
}
