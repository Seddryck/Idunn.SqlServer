using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Model
{
    public class File
    {
        public File(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions ?? new List<Permission>();
        }

        public string Name { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; protected set; }
    }
}
