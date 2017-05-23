using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Model
{
    public class Folder
    {
        public string Path { get; set; }
        public IReadOnlyCollection<File> Files { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; set; }

        public Folder(string path, List<File> files, List<Permission> permissions)
        {
            Path = path;
            Files = files ?? new List<File>();
            Permissions = permissions ?? new List<Permission>();
        }
    }
}
