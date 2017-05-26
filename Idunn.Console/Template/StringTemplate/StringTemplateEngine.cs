using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Template.StringTemplate
{
    public abstract class StringTemplateEngine<T> : IStringTemplateEngine
    {
        public const string RootTemplateName = "root";

        protected virtual TemplateGroup Initialize()
        {
            return Initialize('$', '$');
        }

        protected TemplateGroup Initialize(char start, char stop)
        {
            var group = new TemplateGroup(start, stop);
            group.RegisterRenderer(typeof(string), new StringRenderer());
            return group;
        }

        protected abstract string ReadResource(string textFile);
        
        public string Execute(IEnumerable<object> objects)
        {
            return Execute(objects.Cast<T>());
        }

        public abstract string Execute(IEnumerable<T> objects);

        protected virtual string Execute(TemplateInfo templateInfo, IEnumerable<T> objects)
        {
            var templateInfocollection = new Dictionary<string, TemplateInfo>()
            {
                [RootTemplateName] = templateInfo
            };
            return Execute(templateInfocollection, objects);
        }

        protected virtual string Execute(Dictionary<string, TemplateInfo> templateInfoCollection, IEnumerable<T> objects)
        {
            var group = Initialize();
            var attributes = AssignAttributes(objects);

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

        protected abstract IEnumerable<Dictionary<string, object>> AssignAttributes(IEnumerable<T> objects);
        
    }
}
