using Idunn.Console.Template.StringTemplate;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Template.StringTemplate
{
    public class StringTemplateFactory : ITemplateFactory<Principal>
    {
        public StringTemplateEngine<Principal> Instantiate(string account, bool isInteractive, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                if (isInteractive)
                    return new ImpersonateEngine();
                else
                    return new ConnectUseImpersonateEngine();
            }
            else
                return new ExternalFileEngine(filename);
        }

        IStringTemplateEngine ITemplateFactory.Instantiate(string account, bool isInteractive, string filename)
        {
            return (IStringTemplateEngine)Instantiate(account, isInteractive, filename);
        }

        Type ITemplateFactory.GetRootObjectType()
        {
            return typeof(Principal);
    }
}
}
