using Antlr4.StringTemplate;
using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Template.StringTemplate
{
    public abstract class StringTemplateEngine
    {
        public const string RootTemplateName = "root";

        protected TemplateGroup Initialize()
        {
            var group = new TemplateGroup('$', '$');
            group.RegisterRenderer(typeof(string), new StringRenderer());
            return group;
        }
        
        protected string ReadResource(string textFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Core.Template.StringTemplate.Resources.{textFile}"))
            {
                using (var streamReader = new StreamReader(stream))
                    return streamReader.ReadToEnd();
            }
        }

        public abstract string Execute(IEnumerable<Principal> principals);

        protected virtual string Execute(TemplateInfo templateInfo, IEnumerable<Principal> principals)
        {
            var dico = new Dictionary<string, TemplateInfo>();
            dico.Add(RootTemplateName, templateInfo);
            return Execute(dico, principals);
        }

        protected virtual string Execute(Dictionary<string, TemplateInfo> templateInfoCollection, IEnumerable<Principal> principals)
        {
            var group = Initialize();
            var attributes = AssignAttributes(principals);

            foreach (var templateInfo in templateInfoCollection)
                group.DefineTemplate(templateInfo.Key, templateInfo.Value.Content, templateInfo.Value.Attributes);

            var sb = new StringBuilder();

            foreach (var attribute in attributes)
            {
                var template = group.GetInstanceOf(RootTemplateName);
                foreach (var key in attribute.Keys)
                    template.Add(key, attribute[key]);

                var rendered = template.Render();
                sb.Append(rendered);
            }

            return sb.ToString();
        }

        protected abstract IEnumerable<Dictionary<string, object>> AssignAttributes(IEnumerable<Principal> principals);
        
    }
}
