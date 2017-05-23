using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public interface IEngineParser
    {
        bool IsValid(object node, string name);
        string GetValue(object node, string name);
        IEnumerable<object> GetChildren(object node, string[] names);
        object InstantiateReader(StreamReader streamReader);
        object GetRoot(object reader);
        bool IsRootName(object root, string[] names);
    }
}
