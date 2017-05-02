﻿using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Template.StringTemplate
{
    public class ConnectUseImpersonateEngine : StringTemplateAllInOneEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var dico = new Dictionary<string, string>();
            dico.Add(RootTemplateName, ReadResource("connect-use-impersonate.sql"));
            dico.Add("impersonate", ReadResource("impersonate.sql"));
            var text = Execute(dico, principals);
            return text;
        }
    }
}