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
            return Instantiate(principal, false, string.Empty);
        }

        public StringTemplateEngine Instantiate(string principal, bool isSqlCmd, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                if (string.IsNullOrEmpty(principal))
                    if (isSqlCmd)
                        return new ConnectUseCurrentUserEngine();
                    else
                        return new CurrentUserEngine();
                else
                {
                    if (isSqlCmd)
                        return new ConnectUseImpersonateEngine();
                    else
                        return new ImpersonateEngine();
                }
            }
            else
                return new ExternalFileEngine(filename);
        }
    }
}
