using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IdunnSql.Core.Model
{
    public class ModelFactory
    {
        public Principal Instantiate(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException(string.Format("No file has been found at the location '{0}'.", filename));

            using (var stream = File.OpenRead(filename))
            {
                if (Path.GetExtension(filename) == ".xml")
                    return InstantiateFromXml(stream);
                else
                    throw new NotImplementedException();
            }
        }

        protected Principal InstantiateFromXml(Stream stream)
        {
            var parser = new Parser.XmlParser.Parser();
            return parser.Parse(stream);
        }
    }
}
