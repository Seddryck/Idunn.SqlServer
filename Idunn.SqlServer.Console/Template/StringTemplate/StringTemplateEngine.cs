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
    public abstract class StringTemplateEngine<T> : IStringTemplateEngine
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
        public string Execute(IEnumerable<object> objects)
        {
            return Execute(objects.Cast<T>());
        }


        public abstract string Execute(IEnumerable<T> objects);

        protected virtual string Execute(string templateText, IEnumerable<T> objects)
        {
            var dico = new Dictionary<string, TemplateInfo>();
            dico.Add(RootTemplateName, templateInfo);
            return Execute(dico, objects);
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
