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
            var dico = new Dictionary<string, string>();
            dico.Add(RootTemplateName, templateText);
            return Execute(dico, objects);
        }

        protected virtual string Execute(Dictionary<string, string> templateTexts, IEnumerable<T> objects)
        {
            var group = Initialize();
            var dicos = AssignVariables(objects);

            foreach (var templateName in templateTexts.Keys)
                group.DefineTemplate(templateName, templateTexts[templateName], dicos.ElementAt(0).Keys.ToArray());

            var sb = new StringBuilder();
           

            foreach (var dico in dicos)
            {
                var template = group.GetInstanceOf(RootTemplateName);
                foreach (var key in dico.Keys)
                    template.Add(key, dico[key]);

                var rendered = template.Render();
                sb.Append(rendered);
            }

            return sb.ToString();
        }

        protected abstract IEnumerable<Dictionary<string, object>> AssignVariables(IEnumerable<T> objects);
        
    }
}
