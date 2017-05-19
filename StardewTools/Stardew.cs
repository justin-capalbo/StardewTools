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
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            Console.WriteLine(doc.Elements());

            var elements = doc.Elements();

            //Get "Object" nodes 
            var objects = elements.Descendants("Object");

            int caskCount = 0;
            foreach (var obj in objects)
            {
                //Determine if the "object" is a cask. 
                //Needs heavy refactoring - this is a very brittle solution
                bool cask = false;
                foreach (var desc in obj.Descendants())
                {
                    if (desc.Name == "agingRate")
                    {
                        cask = true;
                        caskCount++;
                    }
                }

                if (cask)
                {
                    var name = obj.Descendants("heldObject").Descendants("DisplayName").First();
                    var days = obj.Descendants("daysToMature").First();
                    Console.WriteLine("==Cask==\nItem: {0}\nDays Left: {1}\n", name, days);
                }
            }
            Console.WriteLine("Total Casks: {0}", caskCount);
            Console.ReadKey();
        }
    }
}
