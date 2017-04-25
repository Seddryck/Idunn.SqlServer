using IdunnSql.Console;
using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Testing.Acceptance
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void Main_Generate_Succesful()
        {
            var source = ResourceOnDisk.CreatePhysicalFile("Sample.xml", "IdunnSql.Testing.Acceptance.Resources.Sample.xml");
            var destination = Path.ChangeExtension(source, ".sql");

            var args = new List<string>();
            args.Add("generate");
            args.Add($"--source={source}");
            args.Add($"--destination={destination}");

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }
        
    }
}
