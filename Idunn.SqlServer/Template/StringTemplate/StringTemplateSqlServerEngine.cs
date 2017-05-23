using Idunn.Console.Template.StringTemplate;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Template.StringTemplate
{
    public abstract class StringTemplateSqlServerEngine : StringTemplateEngine<Principal>

    {
        protected override string ReadResource(string textFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Template.StringTemplate.Resources.{textFile}"))
            {
                using (var streamReader = new StreamReader(stream))
                    return streamReader.ReadToEnd();
            }
        }
    }
}
