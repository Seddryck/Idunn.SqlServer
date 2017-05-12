using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Template.StringTemplate
{
    public interface IStringTemplateEngine
    {
        string Execute(IEnumerable<object> objects);
    }
}
