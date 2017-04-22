using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Model
{
    public class Principal
    {
        public Principal(List<Database> databases)
        {
            Databases = databases;
        }

        public string Name { get; }
        public IReadOnlyCollection<Database> Databases { get; protected set; }

        
    }
}
