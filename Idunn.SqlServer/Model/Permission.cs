using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Model
{
    public class Permission
    {
        public Permission(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
