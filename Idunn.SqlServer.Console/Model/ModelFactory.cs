using Idunn.SqlServer.Console.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Idunn.SqlServer.Console.Model
{
    class ModelFactory
    {
        public IEnumerable<object> Instantiate(string filename, ParserContainer container)
        {
            if (!File.Exists(filename))
                throw new ArgumentException(string.Format("No file has been found at the location '{0}'.", filename));
            var collection = new List<object>();
            using (var stream = File.OpenRead(filename))
            {
                foreach (var rootParser in container.RootParsers)
                {
                    collection.AddRange(rootParser.Parse(stream));
                }
            }

            return collection;
        }
    }
}
