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

        [Test]
        public void Main_GenerateWithImpersonate_Succesful()
        {
            var source = ResourceOnDisk.CreatePhysicalFile("Sample.xml", "IdunnSql.Testing.Acceptance.Resources.Sample.xml");
            var destination = Path.ChangeExtension(source, ".impersonate.sql");

            var args = new List<string>();
            args.Add("generate");
            args.Add($"--source={source}");
            args.Add($"--destination={destination}");
            args.Add($"--principal=Proxy_ETL");

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [Category("AdventureWorksDW2012")]
        public void Main_ExecuteWithCurrentUser_Succesful()
        {
            var source = ResourceOnDisk.CreatePhysicalFile("AdventureWorksDW2012.xml", "IdunnSql.Testing.Acceptance.Resources.AdventureWorksDW2012.xml");

            var args = new List<string>();
            args.Add("execute");
            args.Add($"--source={source}");

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [Category("AdventureWorksDW2012")]
        public void Main_ExecuteWithImpersonation_Succesful()
        {
            var source = ResourceOnDisk.CreatePhysicalFile("AdventureWorksDW2012.xml", "IdunnSql.Testing.Acceptance.Resources.AdventureWorksDW2012.xml");

            var args = new List<string>();
            args.Add("execute");
            args.Add($"--source={source}");
            args.Add($"--principal=COLUMBIA\\cedri");

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        [Category("AdventureWorksDW2012")]
        public void Main_ExecuteWithOutput_Succesful()
        {
            var source = ResourceOnDisk.CreatePhysicalFile("AdventureWorksDW2012.xml", "IdunnSql.Testing.Acceptance.Resources.AdventureWorksDW2012.xml");
            var output = Path.ChangeExtension(source, ".output.txt");

            var args = new List<string>();
            args.Add("execute");
            args.Add($"--source={source}");
            args.Add($"--output={output}");

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }

    }
}
