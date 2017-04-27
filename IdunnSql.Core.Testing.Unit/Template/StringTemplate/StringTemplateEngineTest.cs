using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Testing.Unit.Template.StringTemplate
{
    [TestFixture]
    public class StringTemplateEngineTest
    {
        public class TestableStringTemplateEngine : StringTemplateEngine
        {
            public new string Execute(string template, Principal principal)
            {
                return base.Execute(template, principal);
            }

            public override string Execute(Principal principal)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Execute_Principal_CorrectlyRendered()
        {
            var principal = new Principal("MyUser", new List<Database>() { new Database("db-001", "sql-001", null, null) });
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("Principal = $principal$", principal);
            Assert.That(result, Is.EqualTo("Principal = MyUser"));

        }

        [Test]
        public void Execute_Database_CorrectlyRendered()
        {
            var databases = new List<Database>()
            {
                new Database("db-001", "sql-001", null, null)
                , new Database("db-002", "sql-001", null, null)
            };
            var principal = new Principal("MyUser", databases);
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("Database = $database.server$/$database.name$\r\n", principal);
            Assert.That(result, Is.EqualTo("Database = sql-001/db-001\r\nDatabase = sql-001/db-002\r\n"));
        }

        [Test]
        public void Execute_SecurablePermission_CorrectlyRendered()
        {
            var databases = new List<Database>()
            {
                new Database("db-001", "sql-001", new List<Securable>()
                {
                    new Securable("dbo", "SCHEMA", new List<Permission>() { new Permission("SELECT") })
                    , new Securable("admin.Log", "OBJECT", new List<Permission>() { new Permission("INSERT") })
                }, null)
            };
            var principal = new Principal("MyUser", databases);
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("$securables : { securable|$securable.permission$ on $securable.type$::$securable.name$ for $principal$\r\n}$", principal);
            Assert.That(result, Is.EqualTo("SELECT on SCHEMA::dbo for MyUser\r\nINSERT on OBJECT::admin.Log for MyUser\r\n"));
        }
    }
}
