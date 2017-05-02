using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Model
{
    public class Principal
    {
        public Principal(string name, List<Database> databases)
            : this (databases)
        {
            Name = name;
        }

        public Principal(List<Database> databases)
        {
            Databases = databases ?? new List<Database>();
        }

        public string Name { get; set; }
        public IReadOnlyCollection<Database> Databases { get; protected set; }

        
    }
}
