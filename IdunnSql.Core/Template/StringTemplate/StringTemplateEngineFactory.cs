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
            return Instantiate(principal, string.Empty);
        }

        public StringTemplateEngine Instantiate(string principal, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                if (string.IsNullOrEmpty(principal))
                    return new CurrentUserEngine();
                else
                    return new ImpersonateEngine(principal);
            }
            else
                return new ExternalFileEngine(principal);
        }
    }
}
