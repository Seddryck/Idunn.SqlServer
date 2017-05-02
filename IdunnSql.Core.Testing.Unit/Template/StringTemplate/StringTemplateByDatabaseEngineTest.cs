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
    public class StringTemplateByDatabaseEngineTest
    {
        public class TestableStringTemplateEngine : StringTemplateByDatabaseEngine
        {
            public new string Execute(string template, IEnumerable<Principal> principals)
            {
                return base.Execute(template, principals);
            }

            public override string Execute(IEnumerable<Principal> principals)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Execute_Principal_CorrectlyRendered()
        {
            var principals = Enumerable.Repeat(new Principal("MyUser", new List<Database>() { new Database("db-001", "sql-001", null, null) }), 1);
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("$principal.Name$#$database.name$#$database.server$", principals);
            Assert.That(result, Is.EqualTo("MyUser#db-001#sql-001"));

        }

        [Test]
        public void Execute_Principals_CorrectlyRendered()
        {
            var principals = new List<Principal>();
            principals.Add(new Principal("MyUser", new List<Database>() { new Database("db-001", "sql-001", null, null) }));
            principals.Add(new Principal("MyCopy", new List<Database>() { new Database("db-001", "sql-001", null, null) }));
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("$principal.Name$#$database.name$#$database.server$§", principals);
            Assert.That(result, Is.EqualTo("MyUser#db-001#sql-001§MyCopy#db-001#sql-001§"));

        }

        [Test]
        public void Execute_Database_CorrectlyRendered()
        {
            var databases = new List<Database>()
            {
                new Database("db-001", "sql-001", null, null)
                , new Database("db-002", "sql-001", null, null)
            };
            var principals = Enumerable.Repeat(new Principal("MyUser", databases),1);
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("$principal.Name$#$database.name$#$database.server$§", principals);
            Assert.That(result, Is.EqualTo("MyUser#db-001#sql-001§MyUser#db-002#sql-001§"));
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
            var principals = Enumerable.Repeat(new Principal("MyUser", databases), 1);
            var engine = new TestableStringTemplateEngine();

            var result = engine.Execute("$securables:{securable|$securable.permission$ on $securable.type$::$securable.name$ for $principal.name$\r\n}$", principals);
            Assert.That(result, Is.EqualTo("SELECT on SCHEMA::dbo for MyUser\r\nINSERT on OBJECT::admin.Log for MyUser\r\n"));
        }
    }
}
