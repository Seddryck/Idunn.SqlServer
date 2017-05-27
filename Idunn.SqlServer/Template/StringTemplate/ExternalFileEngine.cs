using Idunn.Core.Template.StringTemplate;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Template.StringTemplate
{
    public class ExternalFileEngine : StringTemplateAllInOneEngine
    {
        private readonly string filename;

        public ExternalFileEngine(string filename)
        {
            this.filename = filename;
        }

        public override string Execute(IEnumerable<Principal> principals)
        {
            if (!File.Exists(filename))
                throw new ArgumentException($"File {filename} not found!");

            var template = File.ReadAllText(filename);
            var templateInfocollection = new Dictionary<string, TemplateInfo>()
            {
                [RootTemplateName] =
                    new TemplateInfo()
                    {
                        Content = template,
                        Attributes = new[] { "principals" }
                    }
            };
            var text = Execute(templateInfocollection, principals);
            return text;
        }
    }
}
