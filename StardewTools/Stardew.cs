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
                    caskCount++;
                    
                    var location = obj.Descendants("tileLocation").First();
                    var x = location.Descendants("X").First().Value;
                    var y = location.Descendants("Y").First().Value;
                    var name = obj.Descendants("heldObject").Descendants("DisplayName").First().Value;
                    var days = obj.Descendants("daysToMature").First().Value;
                    Console.WriteLine("==Cask==\n" +
                                      "Located at: {0},{1}\n" + 
                                      "Item: {2}\n" +
                                      "Days Left: {3}\n", x, y, name, days);
                }
            }
            Console.WriteLine("Total Casks: {0}", caskCount);
            Console.ReadKey();
        }
    }
}
