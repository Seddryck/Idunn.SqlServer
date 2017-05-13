using Idunn.Console.Template.StringTemplate;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Template.StringTemplate
{
    public class StringTemplateFactoryRegister : ITemplateFactoryRegister<Principal>
    {
        public ITemplateFactory<Principal> GetFactory()
        {
            return new StringTemplateFactory();
        }
    }
}
