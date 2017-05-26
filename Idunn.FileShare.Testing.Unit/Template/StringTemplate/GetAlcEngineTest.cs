using Idunn.Console.Template.StringTemplate;
using Idunn.FileShare.Model;
using Idunn.FileShare.Template.StringTemplate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Testing.Unit.Template.StringTemplate
{
    [TestFixture]
    public class GetAclTest
    {
        [Test]
        public void Execute_Account_CorrectlyRendered()
        {
            var accounts = Enumerable.Repeat(
                new Account(@"domain\user-001",
                    new List<Folder>()
                    {
                        new Folder(@"c:\folder-001\",
                            new List<File>()
                            {
                                new File("file-001.txt",
                                    new List<Permission>()
                                    {
                                        new Permission("Read")
                                    })
                            }, null)
                    }), 1);
            var engine = new GetAclEngine();

            var result = engine.Execute(accounts);
            var expected = ResourceOnMemory.GetContent("Idunn.FileShare.Testing.Unit.Template.StringTemplate.Resources.Sample.expected.ps1");
            Assert.That(result, Is.EqualTo(expected));
        }
        
    }
}
