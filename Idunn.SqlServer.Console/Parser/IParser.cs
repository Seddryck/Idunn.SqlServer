﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Console.Parser
{
    public interface IParser<T>
    {
        T Parse(object node);
    }
}
