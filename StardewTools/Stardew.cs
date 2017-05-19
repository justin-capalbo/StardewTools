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
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            
            int caskCount = 0;
            foreach (var obj in doc.Descendants("Object"))
            {
                string objType = (string)obj.Attribute(xsi + "type");
                if (objType == "Cask")
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
