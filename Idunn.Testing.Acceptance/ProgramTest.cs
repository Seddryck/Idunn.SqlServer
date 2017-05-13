using Idunn.Console;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Testing.Acceptance
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        [TestCase("xml")]
        [TestCase("yml")]
        public void Main_Generate_Succesful(string extension)
        {
            var source = ResourceOnDisk.CreatePhysicalFile($"Sample.{extension}", $"Idunn.Testing.Acceptance.Resources.Sample.{extension}");
            var destination = Path.ChangeExtension(source, $".{extension}.sql");

            var args = new List<string>()
            {
                "generate",
                $"--source={source}",
                $"--destination={destination}"
            };


            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        [TestCase("xml")]
        [TestCase("yml")]
        public void Main_GenerateExternalTemplate_Succesful(string extension)
        {
            var source = ResourceOnDisk.CreatePhysicalFile($"Sample.{extension}", $"Idunn.Testing.Acceptance.Resources.Sample.{extension}");
            var template = ResourceOnDisk.CreatePhysicalFile("MarkdownTemplate.md", "Idunn.Testing.Acceptance.Resources.MarkdownTemplate.md");
            var destination = Path.ChangeExtension(source, $".{extension}.output.md");

            var args = new List<string>()
            {
                "generate",
                $"--source={source}",
                $"--destination={destination}",
                $"--template={template}"
            };

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));

            var expected = ResourceOnMemory.GetContent("Idunn.Testing.Acceptance.Resources.Sample.expected.md");
            var actual = File.ReadAllText(destination);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Category("AdventureWorksDW2012")]
        [TestCase("xml")]
        [TestCase("yml")]
        public void Main_GenerateWithImpersonation_Succesful(string extension)
        {
            var source = ResourceOnDisk.CreatePhysicalFile($"AdventureWorksDW2012.{extension}", $"Idunn.Testing.Acceptance.Resources.AdventureWorksDW2012.{extension}");
            var destination = Path.ChangeExtension(source, $".{extension}.sql");

            var args = new List<string>()
            {
                "generate",
                $"--source={source}",
                $"--destination={destination}",
            };
            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));

        }

        [Test]
        [Category("AdventureWorksDW2012")]
        [TestCase("xml")]
        [TestCase("yml")]
        public void Main_ExecuteWithImpersonation_Succesful(string extension)
        {
            var source = ResourceOnDisk.CreatePhysicalFile($"AdventureWorksDW2012.{extension}", $"Idunn.Testing.Acceptance.Resources.AdventureWorksDW2012.{extension}");

            var args = new List<string>()
            {
                "execute",
                $"--source={source}"
            };
            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));

        }
        
        [Test]
        [Category("AdventureWorksDW2012")]
        [TestCase("xml")]
        [TestCase("yml")]
        public void Main_ExecuteWithOutput_Succesful(string extension)
        {
            var source = ResourceOnDisk.CreatePhysicalFile($"AdventureWorksDW2012.{extension}", $"Idunn.Testing.Acceptance.Resources.AdventureWorksDW2012.{extension}");
            var output = Path.ChangeExtension(source, $"{extension}.output.txt");

            var args = new List<string>()
            {
                "execute",
                $"--source={source}",
                $"--output={output}"
            };

            var result = Program.Main(args.ToArray());
            Assert.That(result, Is.EqualTo(0));

            var expected = ResourceOnMemory.GetContent("Idunn.Testing.Acceptance.Resources.AdventureWorksDW2012.expected.txt");
            var actual = File.ReadAllText(output);
            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
