using Antlr4.StringTemplate;
using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public abstract class StringTemplateEngine
    {
        protected TemplateGroup Initialize()
        {
            var group = new TemplateGroup('$', '$');
            group.RegisterRenderer(typeof(string), new StringRenderer());
            return group;
        }
        
        protected string ReadResource(string textFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"IdunnSql.Core.Template.StringTemplate.Resources.{textFile}"))
            {
                using (var streamReader = new StreamReader(stream))
                    return streamReader.ReadToEnd();
            }
        }

        public abstract string Execute(Principal principal, bool isSqlCmd);

        protected virtual string Execute(string templateText, Principal principal, bool isSqlCmd)
        {
            var group = Initialize();

            var sb = new StringBuilder();
            var dicos = AssignVariables(principal);

            foreach (var dico in dicos)
            {
                var template = new Antlr4.StringTemplate.Template(group, templateText);
                template.Add("sqlcmd", isSqlCmd);
                foreach (var key in dico.Keys)
                    template.Add(key, dico[key]);

                var rendered = template.Render();
                sb.Append(rendered);
            }

            return sb.ToString();
        }

        protected IEnumerable<Dictionary<string, object>> AssignVariables(Principal principal)
        {
            var principalDto = principal.Name;
            foreach (var database in principal.Databases)
            {
                var databaseDto = new { Name = database.Name, Server = database.Server };
                var securablesDto = new List<object>();
                foreach (var permission in database.Permissions)
                    securablesDto.Add(new { Type = "DATABASE", Name = database.Name, Permission = permission.Name });

                foreach (var securable in database.Securables)
                    foreach (var permission in securable.Permissions)
                        securablesDto.Add(new { Type = securable.Type, Name = securable.Name, Permission = permission.Name });

                var dico = new Dictionary<string, object>();
                dico.Add("principal", principalDto);
                dico.Add("database", databaseDto);
                dico.Add("securables", securablesDto);

                yield return dico;
            }
        }

    }
}
