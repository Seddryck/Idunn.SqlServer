using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Template.StringTemplate
{
    public class ExternalFileEngine : StringTemplateAllInOneEngine
    {
        private readonly string filename;

        public ExternalFileEngine(string filename)
        {
            this.filename = filename;
        }

        public override string Execute(IEnumerable<Principal> principals)
        {
            if (!File.Exists(filename))
                throw new ArgumentException($"File {filename} not found!");

            var template = File.ReadAllText(filename);
            var text = Execute(template, principals);
            return text;
        }
    }
}
