using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Model
{
    public class Securable
    {
        public Securable(string name, string type, List<Permission> permissions)
        {
            Name = name;
            Type = type;
            Permissions = permissions;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; protected set; }
    }
}
