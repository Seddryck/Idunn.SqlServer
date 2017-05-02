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
    public abstract class StringTemplateAllInOneEngine : StringTemplateEngine
    {
        protected override IEnumerable<Dictionary<string, object>> AssignVariables(IEnumerable<Principal> principals)
        {
            var principalsDto = new List<object>();
            foreach (var principal in principals)
            {
                var databasesDto = new List<object>();
                foreach (var database in principal.Databases)
                {
                    
                    var securablesDto = new List<object>();
                    foreach (var permission in database.Permissions)
                        securablesDto.Add(new { Type = "DATABASE", Name = database.Name, Permission = permission.Name });

                    foreach (var securable in database.Securables)
                        foreach (var permission in securable.Permissions)
                            securablesDto.Add(new { Type = securable.Type, Name = securable.Name, Permission = permission.Name });

                    var databaseDto = new { Name = database.Name, Server = database.Server, Securables = securablesDto };
                    databasesDto.Add(databaseDto);
                }
                var principalDto = new { Name = principal.Name, Databases = databasesDto };
                principalsDto.Add(principalDto);
            }

            var dico = new Dictionary<string, object>();
            dico.Add("principals", principalsDto);

            yield return dico;
        }
    }
}
