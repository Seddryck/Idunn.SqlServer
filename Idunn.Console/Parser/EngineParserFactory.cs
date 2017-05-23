using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public class EngineParserFactory
    {
        public IEngineParser Instantiate(string extension)
        {
            if (extension == ".xml")
                return new XmlEngineParser();
            else if (extension == ".yml")
                return new YamlEngineParser();

            throw new ArgumentOutOfRangeException();
        }
    }
}
