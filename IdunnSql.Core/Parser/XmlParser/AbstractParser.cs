using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
{
    public abstract class AbstractParser<T> : IParser<T>
    {
        protected readonly ParserFactory factory;

        public AbstractParser(ParserFactory factory)
        {
            this.factory = factory;
        }

        public abstract T Parse(XmlNode node);

        protected List<S> ParseChildren<S>(XmlNode node, string name)
        {
            var children = new List<S>();
            foreach (XmlNode child in node.ChildNodes.Cast<XmlNode>().Where(n => n.Name == name))
            {
                var parser = factory.Retrieve<S>();
                children.Add(parser.Parse(child));
            }
            return children;
        }
    }
}
