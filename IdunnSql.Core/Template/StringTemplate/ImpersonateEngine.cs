using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class ImpersonateEngine : StringTemplateEngine
    {
        private readonly string principalName;

        public ImpersonateEngine(string principalName)
        {
            this.principalName = principalName;
        }

        public override string Execute(Principal principal, bool isSqlCmd)
        {
            var template = ReadResource("impersonate.sql");
            principal.Name = principalName;
            var text = Execute(template, principal, isSqlCmd);
            return text;
        }
    }
}
