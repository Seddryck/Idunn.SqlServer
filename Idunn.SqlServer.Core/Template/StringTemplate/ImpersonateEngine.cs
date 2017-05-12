using Idunn.SqlServer.Console.Template.StringTemplate;
using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Template.StringTemplate
{
    public class ImpersonateEngine : StringTemplateByDatabaseEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var templateInfocollection = new Dictionary<string, TemplateInfo>();
            templateInfocollection.Add(RootTemplateName,
                new TemplateInfo()
                {
                    Content = ReadResource("impersonate.sql")
                        ,
                    Attributes = new[] { "principal", "database", "securables" }
                });
            var text = Execute(templateInfocollection, principals);
            return text;
        }
    }
}
