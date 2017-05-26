using Antlr4.StringTemplate;
using Idunn.Console.Template.StringTemplate;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Template.StringTemplate
{
    public class GetAclEngine : StringTemplateFileShareEngine
    {
        protected override TemplateGroup Initialize()
        {
            return Initialize('<', '>');
        }

        public override string Execute(IEnumerable<Account> accounts)
        {
            var templateInfocollection = new Dictionary<string, TemplateInfo>()
            {
                [RootTemplateName] =
                    new TemplateInfo()
                    {
                        Content = ReadResource("get-acl - Container.ps1"),
                        Attributes = new[] { "accounts" }
                    },
                ["getacl"] =
                    new TemplateInfo()
                    {
                        Content = ReadResource("get-acl.ps1"),
                        Attributes = new[] { "account", "path", "permission" }
                    }
            };
            var text = Execute(templateInfocollection, accounts);
            return text;
        }

        protected override IEnumerable<Dictionary<string, object>> AssignAttributes(IEnumerable<Account> accounts)
        {
            var accountsDto = new List<object>();
            foreach (var account in accounts)
            {
                var securablesDto = new List<object>();
                foreach (var folder in account.Folders)
                {
                    foreach (var permission in folder.Permissions)
                        securablesDto.Add(new { Path = folder.Path, Permission = permission.Name });

                    foreach (var file in folder.Files)
                        foreach (var permission in file.Permissions)
                            securablesDto.Add(new { Path = folder.Path + file.Name, Permission = permission.Name });
                }
                var accountDto = new { Account = account.Name, Securables = securablesDto };
                accountsDto.Add(accountDto);
            }

            var dico = new Dictionary<string, object>()
            {
                ["accounts"] = accountsDto
            };

            yield return dico;
        }
    }
}
