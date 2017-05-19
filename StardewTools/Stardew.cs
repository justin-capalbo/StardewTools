using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

namespace StardewTools
{
    class Stardew
    {
        static void Main(string[] args)
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\docs\";
            var doc = XDocument.Load(folder + "Jectia.xml");

            Console.WriteLine(doc.Elements());

            foreach (var child in doc.Element("Cask").Elements())
            {
                Console.WriteLine(child.Name);
            }
        }
    }
}
