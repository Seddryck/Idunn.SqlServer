using Idunn.Console.Template.StringTemplate;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Template.StringTemplate
{
    public class StringTemplateFactoryRegister : ITemplateFactoryRegister<Account>
    {
        public ITemplateFactory<Account> GetFactory()
        {
            return new StringTemplateFactory();
        }
    }
}
