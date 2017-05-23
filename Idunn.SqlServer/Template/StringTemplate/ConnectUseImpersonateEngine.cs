using Idunn.Console.Template.StringTemplate;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Template.StringTemplate
{
    public class ConnectUseImpersonateEngine : StringTemplateAllInOneEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var templateInfocollection = new Dictionary<string, TemplateInfo>()
            {
                [RootTemplateName] =  
                    new TemplateInfo()
                    {
                        Content = ReadResource("connect-use-impersonate.sql"),
                        Attributes = new[] { "principals" }
                    },
                ["impersonate"] = 
                    new TemplateInfo()
                    {
                        Content = ReadResource("impersonate.sql"),
                        Attributes = new[] { "principal", "securables" }
                    }
            };
            var text = Execute(templateInfocollection, principals);
            return text;
        }
    }
}
