﻿using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Parser.YamlParser
{
    class SecurableParser : AbstractYamlParser<Securable>
    {
        public SecurableParser(IParserContainer container)
            : base(container)
        {
        }

        public override Securable Parse(YamlNode node)
        {
            if (node.NodeType != YamlNodeType.Mapping)
                throw new ArgumentException();

            var mapping = (YamlMappingNode)node;

            var name = GetValue(mapping, "name");
            var type = GetValue(mapping, "type");

            var permissions = ParseChildren<Permission>(mapping, "permission", "permissions");

            var securable = new Securable(name, type, permissions);
            return securable;

        }
    }
}