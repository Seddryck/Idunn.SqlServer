﻿using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Template.StringTemplate
{
    public class CurrentUserEngine : StringTemplateByDatabaseEngine
    {
        public override string Execute(IEnumerable<Principal> principals)
        {
            var template = ReadResource("current-user.sql");
            
            var text = Execute(template, principals);
            return text;
        }
        
    }
}