using Idunn.Console.Template.StringTemplate;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Template.StringTemplate
{
    public class StringTemplateFactory : ITemplateFactory<Account>
    {
        public StringTemplateEngine<Account> Instantiate(string account, bool isInteractive, string filename)
        {
            return new GetAclEngine();
        }

        IStringTemplateEngine ITemplateFactory.Instantiate(string account, bool isInteractive, string filename)
        {
            return (IStringTemplateEngine)Instantiate(account, isInteractive, filename);
        }

        Type ITemplateFactory.GetRootObjectType()
        {
            return typeof(Account);
        }
    }
}
