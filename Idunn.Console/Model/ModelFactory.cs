﻿using Idunn.Console.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Idunn.Console.Model
{
    class ModelFactory
    {
        public IEnumerable<object> Instantiate(string filename, ParserContainer container)
        {
            if (!File.Exists(filename))
                throw new ArgumentException(string.Format("No file has been found at the location '{0}'.", filename));
            var collection = new List<object>();
            foreach (var rootParser in container.RootParsers)
            {
                using (var stream = File.OpenRead(filename))
                    collection.AddRange(((IRootParser)rootParser).Parse(stream));
            }

            return collection;
        }
    }
}
