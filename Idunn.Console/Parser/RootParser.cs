using Idunn.Console.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.Console.Parser
{
    public abstract class RootParser<T> : IRootParser<T>, IRootParser
    {
        protected readonly IParserContainer container;
        protected readonly IEngineParser engine;

        protected abstract string[] GetRootNames();

        public RootParser(IParserContainer container, IEngineParser engine)
        {
            this.container = container;
            this.engine = engine;
        }

        public IEnumerable<T> Parse(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var reader = engine.InstantiateReader(streamReader);
                var root = engine.GetRoot(reader);

                var rootObjects = new List<T>();
                var parser = container.Retrieve<T>();
                if (engine.IsRootName(root, GetRootNames()))
                {
                    var rootObject = parser.Parse(root);
                    rootObjects.Add((T)rootObject);
                }
                else if (engine.IsRootName(root, new[] { "idunn" }))
                {
                    foreach (var child in engine.GetChildren(root, GetRootNames()))
                    {
                        var principal = parser.Parse(child);
                        rootObjects.Add((T)principal);
                    }
                }

                if (reader is IDisposable)
                    ((IDisposable)reader).Dispose();

                return rootObjects;
            }
        }

        IEnumerable<object> IRootParser.Parse(Stream stream)
        {
            return Parse(stream).Cast<object>();
        }
    }
}
