using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Template.StringTemplate
{
    public interface ITemplateFactory
    {
        IStringTemplateEngine Instantiate(string account, bool isInteractive, string filename);
        Type GetRootObjectType();
    }

    public interface ITemplateFactory<T> : ITemplateFactory
    {
        new StringTemplateEngine<T> Instantiate(string account, bool isInteractive, string filename);
    }
}
