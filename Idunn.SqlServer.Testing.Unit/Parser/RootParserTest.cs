using Idunn.SqlServer.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using Idunn.Core.Parser;
using Idunn.SqlServer.Parser;

namespace Idunn.SqlServer.Testing.Unit.Parser
{
    [TestFixture]
    public class RootParserTest
    {
        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_Valid_CorrectlyParsedPrincipal(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principals = parser.Parse(stream);
                Assert.That(principals, Is.Not.Null);
                Assert.That(principals, Is.AssignableTo<IEnumerable<Principal>>());
                Assert.That(principals, Has.Count.EqualTo(1));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_Valid_CorrectlyParsedDatabases(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                Assert.That(principal.Databases, Is.Not.Null.And.Not.Empty);
                Assert.That(principal.Databases, Has.Count.EqualTo(2));
                Assert.That(principal.Databases.All(d => d.Server == "sql-001"));
                Assert.That(principal.Databases.Any(d => d.Name == "db-001"));
                Assert.That(principal.Databases.Any(d => d.Name == "db-002"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_Valid_CorrectlyParsedDatabasePermissions(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                var db = principal.Databases.Single(d => d.Name == "db-001");
                Assert.That(db.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(db.Permissions, Has.Count.EqualTo(1));
                Assert.That(db.Permissions.First().Name, Is.EqualTo("CONNECT"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_Valid_CorrectlyParsedSecurables(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                var db = principal.Databases.Single(d => d.Name == "db-001");
                Assert.That(db.Securables, Is.Not.Null.And.Not.Empty);
                Assert.That(db.Securables, Has.Count.EqualTo(2));
                Assert.That(db.Securables.All(s => s.Type == "schema"));
                Assert.That(db.Securables.Any(s => s.Name == "dbo"));
                Assert.That(db.Securables.Any(s => s.Name == "admin"));
            }
        }

        [Test]
        [TestCase(".xml")]
        [TestCase(".yml")]
        public void Parse_Valid_CorrectlySecurablePermissions(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Sample{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                var db = principal.Databases.Single(d => d.Name == "db-001");
                var securable = db.Securables.Single(s => s.Name == "dbo");
                Assert.That(securable.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(securable.Permissions, Has.Count.EqualTo(2));
                Assert.That(securable.Permissions.Any(s => s.Name == "SELECT"));
                Assert.That(securable.Permissions.Any(s => s.Name == "UPDATE"));
            }
        }

        [Test]
        [TestCase(".xml")]
        //[TestCase(".yml")]
        public void Parse_ValidWithMultiplePrincipals_CorrectlyPrincipals(string extension)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"Idunn.SqlServer.Testing.Unit.Parser.Resources.Multiple{extension}"))
            {
                var register = new ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register, extension);

                var parser = register.GetRootParser();
                var principals = (parser.Parse(stream) as IEnumerable<Principal>);

                Assert.That(principals, Is.Not.Null);
                Assert.That(principals, Has.Count.EqualTo(2));
                Assert.That(principals.Any(p => p.Name == "ExecuteDwh"));
                Assert.That(principals.Any(p => p.Name == "Logger"));
                Assert.That(principals.All(p => p.Databases.Count>=1));
            }
        }
    }
}
