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
            string[,] caskData = new string[20,20];
            foreach (var obj in doc.Descendants("Object"))
            {
                string objType = (string)obj.Attribute(xsi + "type");
                if (objType == "Cask")
                {
                    caskCount++;
                    
                    //Data about our cask
                    var location = obj.Descendants("tileLocation").First();
                    var x = location.Descendants("X").First().Value;
                    var y = location.Descendants("Y").First().Value;
                    var agingRate = Double.Parse(obj.Descendants("agingRate").First().Value);
                    var dayNum = Math.Round(Double.Parse(obj.Descendants("daysToMature").First().Value) / agingRate, 2);
                    var days = dayNum.ToString();

                    //Data about what's in the cask
                    var heldObject = obj.Descendants("heldObject");
                    var name = heldObject.Descendants("DisplayName").First().Value;
                    string quality = QualityConv(heldObject.Descendants("quality").First().Value);

                    //Visually, we need this to be y, x
                    caskData[Int32.Parse(y), Int32.Parse(x)] = String.Format("{0} {1}: {2}", quality, name, days);

                    //Console "Debugging"
                    Console.WriteLine("==Cask==\n" +
                                      "Located at: ({0},{1})\n" + 
                                      "Item: {4} {2}\n" +
                                      "Days Left: {3}\n", x, y, name, days, quality);
                }
            }
            Console.WriteLine("Total Casks: {0}", caskCount);
            OutputCSV(caskData, "casks");
            Console.ReadKey();
        }

        private static String QualityConv(string quality)
        {
            switch(quality)
            {
                case "0": return "N.";
                case "1": return "S.";
                case "2": return "G.";
                case "3": return "I.";
            }
            return "";
        }

        private static void OutputCSV(string[,] data, string file = "test")
        {
            using (StreamWriter outfile = new StreamWriter(String.Format("C:/Temp/{0}.csv", file)))
            {
                for (int x = 0; x < data.GetLength(0); x++)
                {
                    string content = "";
                    for (int y = 0; y < data.GetLength(1); y++)
                    {
                        content += data[x, y] + ",";
                    }
                    outfile.WriteLine(content);
                }
            }
        }
        
    }
}
