using Idunn.Console.Template.StringTemplate;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Template.StringTemplate
{
    public abstract class StringTemplateFileShareEngine : StringTemplateEngine<Account>

    {
        protected override string ReadResource(string textFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Template.StringTemplate.Resources.{textFile}"))
            {
                using (var streamReader = new StreamReader(stream))
                    return streamReader.ReadToEnd();
            }
        }
    }
}
