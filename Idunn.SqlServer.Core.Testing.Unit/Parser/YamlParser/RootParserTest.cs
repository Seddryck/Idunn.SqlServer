using Idunn.SqlServer.Core.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Idunn.SqlServer.Console.Parser;

namespace Idunn.SqlServer.Core.Testing.Acceptance.Parser.YamlParser
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void Parse_ValidYaml_CorrectlyParsedPrincipal()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

                var parser = register.GetRootParser();
                var principals = parser.Parse(stream);
                Assert.That(principals, Is.Not.Null);
                Assert.That(principals, Is.AssignableTo<IEnumerable<Principal>>());
                Assert.That(principals, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void Parse_ValidYaml_CorrectlyParsedDatabases()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

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
        public void Parse_ValidYaml_CorrectlyParsedDatabasePermissions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                var db = principal.Databases.Single(d => d.Name == "db-001");
                Assert.That(db.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(db.Permissions, Has.Count.EqualTo(1));
                Assert.That(db.Permissions.First().Name, Is.EqualTo("CONNECT"));
            }
        }

        [Test]
        public void Parse_ValidYaml_CorrectlyParsedSecurables()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

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
        public void Parse_ValidYaml_CorrectlySecurablePermissions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

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
        public void Parse_ValidCondensatedYaml_CorrectlySecurablePermissions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Sample - Condensated.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

                var parser = register.GetRootParser();
                var principal = (parser.Parse(stream).ElementAt(0) as Principal);

                var db = principal.Databases.Single(d => d.Name == "db-001");
                var securable = db.Securables.Single(s => s.Name == "dbo");
                Assert.That(securable.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(securable.Permissions, Has.Count.EqualTo(2));
                Assert.That(securable.Permissions.Any(s => s.Name == "SELECT"));
                Assert.That(securable.Permissions.Any(s => s.Name == "UPDATE"));

                securable = db.Securables.Single(s => s.Name == "admin");
                Assert.That(securable.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(securable.Permissions, Has.Count.EqualTo(1));
                Assert.That(securable.Permissions.Any(s => s.Name == "INSERT"));

                db = principal.Databases.Single(d => d.Name == "db-002");
                securable = db.Securables.Single(s => s.Name == "dbo.Calculate");
                Assert.That(securable.Permissions, Is.Not.Null.And.Not.Empty);
                Assert.That(securable.Permissions, Has.Count.EqualTo(1));
                Assert.That(securable.Permissions.Any(s => s.Name == "EXECUTE"));
            }
        }

        [Test]
        public void Parse_ValidYamlWithMultiplePrincipals_CorrectlyPrincipals()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Idunn.SqlServer.Core.Testing.Unit.Parser.YamlParser.Resources.Multiple.yml"))
            {
                var register = new Core.Parser.YamlParser.ParserRegister();
                var container = new ParserContainer();
                container.Initialize(register);

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
