using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Parser.XmlParser
{
    public class ParserFactory
    {
        private Dictionary<Type, object> definitions;

        public void Initialize()
        {
            definitions = new Dictionary<Type, object>();
            definitions.Add(typeof(Principal), new PrincipalParser(this));
            definitions.Add(typeof(Database), new DatabaseParser(this));
            definitions.Add(typeof(Permission), new PermissionParser(this));
            definitions.Add(typeof(Securable), new SecurableParser(this));
        }

        public IParser<T> Retrieve<T>()
        {
            if (definitions.Keys.Contains(typeof(T)))
                return definitions[typeof(T)] as IParser<T>;

            throw new ArgumentException();
        }
    }
}
