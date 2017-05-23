using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Model
{
    public class Account
    {
        public Account(string name, List<Folder> shares)
        {
            Name = name;
            Folders = shares ?? new List<Folder>();
        }
        
        public string Name { get; set; }
        public IReadOnlyCollection<Folder> Folders { get; protected set; }

        
    }
}
