using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public abstract class AbstractParser<T> : IParser
    {
        protected readonly IParserContainer container;
        protected readonly IEngineParser engine;

        public AbstractParser(IParserContainer container, IEngineParser engine)
        {
            this.container = container;
            this.engine = engine;
        }

        object IParser.Parse(object node)
        {
            return Parse(node);
        }

        public abstract T Parse(object node);

        protected List<S> ParseChildren<S>(object node, string[] names)
        {
            var children = new List<S>();
            foreach (var child in engine.GetChildren(node, names))
            {
                var parser = container.Retrieve<S>();
                children.Add((S)parser.Parse(child));
            }
            return children;
        }

        
    }
}
