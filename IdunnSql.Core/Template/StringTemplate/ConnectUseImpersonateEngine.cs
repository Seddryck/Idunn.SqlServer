using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class ConnectUseImpersonateEngine : StringTemplateEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var dico = new Dictionary<string, string>();
            dico.Add(RootTemplateName, ReadResource("connect-use-impersonate.sql"));
            dico.Add("impersonate", ReadResource("impersonate.sql"));
            var text = Execute(dico, principals);
            return text;
        }
    }
}
