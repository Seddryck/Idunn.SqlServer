using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Parser
{
    class FileParserAttribute : Attribute
    {
        public string Extension { get; set; }
    }
}
