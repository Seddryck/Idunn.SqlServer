using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Execution
{
    public class ExecutorAttribute : Attribute
    {
        public Type Type { get; set; }

        public ExecutorAttribute(Type type)
        {
            Type = type;
        }
    }
}
