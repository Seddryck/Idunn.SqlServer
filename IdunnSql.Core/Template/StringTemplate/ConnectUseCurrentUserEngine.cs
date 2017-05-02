using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class ConnectUseCurrentUserEngine : StringTemplateAllInOneEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var dico = new Dictionary<string, string>();
            dico.Add(RootTemplateName, ReadResource("connect-use-current-user.sql"));
            dico.Add("current_user", ReadResource("current-user.sql"));
            var text = Execute(dico, principals);
            return text;
        }
    }
}
