using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Idunn.SqlServer.Core.Model
{
    public class ModelFactory
    {
        public IEnumerable<Principal> Instantiate(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException(string.Format("No file has been found at the location '{0}'.", filename));

            using (var stream = File.OpenRead(filename))
            {
                if (Path.GetExtension(filename) == ".xml")
                    return InstantiateFromXml(stream);
                if (Path.GetExtension(filename) == ".yml" || Path.GetExtension(filename) == ".yaml")
                    return InstantiateFromYaml(stream);
                else
                    throw new NotImplementedException();
            }
        }

        protected IEnumerable<Principal> InstantiateFromXml(Stream stream)
        {
            var factory = new Parser.XmlParser.ParserFactory();
            factory.Initialize();

            var parser = new Parser.XmlParser.RootParser(factory);
            return parser.Parse(stream);
        }

        protected IEnumerable<Principal> InstantiateFromYaml(Stream stream)
        {
            var factory = new Parser.YamlParser.ParserFactory();
            factory.Initialize();

            var parser = new Parser.YamlParser.RootParser(factory);
            return parser.Parse(stream);
        }
    }
}
