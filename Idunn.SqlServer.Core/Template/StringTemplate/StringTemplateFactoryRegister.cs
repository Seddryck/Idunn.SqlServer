using Idunn.SqlServer.Console.Template.StringTemplate;
using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Template.StringTemplate
{
    public class StringTemplateFactoryRegister : ITemplateFactoryRegister<Principal>
    {
        public ITemplateFactory<Principal> GetFactory()
        {
            return new StringTemplateFactory();
        }
    }
}
