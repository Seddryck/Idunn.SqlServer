using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class StringTemplateEngineFactory
    {
        public StringTemplateEngine Instantiate(string principal)
        {
            if (string.IsNullOrEmpty(principal))
                return new CurrentUserEngine();
            else
                return new ImpersonateEngine(principal);

        }
    }
}
