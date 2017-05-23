using Idunn.FileShare.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using Idunn.Console.Parser;

namespace Idunn.FileShare.Testing.Acceptance.Parser.XmlParser
{
    [TestFixture]
    public class RootParserTest
    {
        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_ValidXml_CorrectlyParsedAccount(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var accounts = parser.Parse(stream);
                Assert.That(accounts, Is.Not.Null);
                Assert.That(accounts, Is.AssignableTo<IEnumerable<Account>>());
                Assert.That(accounts, Has.Count.EqualTo(1));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_ValidXml_CorrectlyParsedFolders(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var account = (parser.Parse(stream).ElementAt(0) as Account);

                Assert.That(account.Folders, Is.Not.Null.And.Not.Empty);
                Assert.That(account.Folders, Has.Count.EqualTo(2));
                Assert.That(account.Folders.Any(f => f.Path == "c:\\folder-001"));
                Assert.That(account.Folders.Any(f => f.Path == "c:\\folder-002"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_ValidXml_CorrectlyParsedFolderPermissions(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var account = (parser.Parse(stream).ElementAt(0) as Account);

                var folder = account.Folders.Single(f => f.Path == "c:\\folder-001");
                Assert.That(folder.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(folder.Permissions, Has.Count.EqualTo(1));
                Assert.That(folder.Permissions.First().Name, Is.EqualTo("LIST"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_ValidXml_CorrectlyParsedFiles(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var account = (parser.Parse(stream).ElementAt(0) as Account);

                var folder = account.Folders.Single(d => d.Path == "c:\\folder-001");
                Assert.That(folder.Files, Is.Not.Null.And.Not.Empty);
                Assert.That(folder.Files, Has.Count.EqualTo(2));
                Assert.That(folder.Files.Any(s => s.Name == "file-001.txt"));
                Assert.That(folder.Files.Any(s => s.Name == "file-002.txt"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_ValidXml_CorrectlyFilePermissions(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var account = (parser.Parse(stream).ElementAt(0) as Account);

                var folder = account.Folders.Single(f => f.Path == "c:\\folder-001");
                var file = folder.Files.Single(s => s.Name == "file-001.txt");
                Assert.That(file.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(file.Permissions, Has.Count.EqualTo(2));
                Assert.That(file.Permissions.Any(s => s.Name == "READ"));
                Assert.That(file.Permissions.Any(s => s.Name == "WRITE"));
            }
        }

        [Test]
        [TestCase(".xml")]
        //[TestCase(".yml")]
        public void Parse_ValidXmlWithMultiplePrincipals_CorrectlyPrincipals(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.FileShare.Testing.Unit.Parser.Resources.Multiple{extension}"))
            {
                var register = new FileShare.Parser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var accounts = (parser.Parse(stream) as IEnumerable<Account>);

                Assert.That(accounts, Is.Not.Null);
                Assert.That(accounts, Has.Count.EqualTo(2));
                Assert.That(accounts.Any(a => a.Name == "domain\\account-001"));
                Assert.That(accounts.Any(a => a.Name == "domain\\account-002"));
                Assert.That(accounts.All(a => a.Folders.Count>=1));
            }
        }
    }
}
