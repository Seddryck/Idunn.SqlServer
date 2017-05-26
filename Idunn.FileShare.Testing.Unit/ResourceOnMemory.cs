using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Testing.Unit
{
    class ResourceOnMemory
    {
        
        public static string GetContent(string resource)
        {
            // A Stream is needed to read the document.
            using (Stream stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream(resource))
            {
                if (stream == null)
                    throw new FileNotFoundException(resource);

                using (StreamReader reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}

