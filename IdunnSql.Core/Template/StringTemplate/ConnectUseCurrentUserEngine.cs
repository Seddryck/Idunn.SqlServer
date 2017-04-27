using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class ConnectUseCurrentUserEngine : StringTemplateEngine
    {
        public override string Execute(Principal principal)
        {
            var dico = new Dictionary<string, string>();
            dico.Add(RootTemplateName, ReadResource("connect-use-current-user.sql"));
            dico.Add("current_user", ReadResource("current-user.sql"));
            var text = Execute(dico, principal);
            return text;
        }
    }
}
