using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Template.StringTemplate
{
    public interface ITemplateFactoryRegister<T>
    {
        ITemplateFactory<T> GetFactory();
    }
}
