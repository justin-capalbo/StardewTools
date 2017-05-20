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
        private static XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

        static void Main(string[] args)
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Docs\";
            var saveFile = XDocument.Load(folder + "Jectia.xml");

            OutputBasement(saveFile);
        }

        private static void OutputBasement(XDocument saveFile)
        {
            string[,] basement = new string[20, 20];
            int dy = 1, dx = 5;

            var caskObjects = saveFile.Descendants("Object").Where(o => (string)o.Attribute(xsi + "type") == "Cask");

            List<Cask> casks = Cask.BuildCaskList(caskObjects);
            foreach (Cask cask in casks)
            {
                //Formatted with line break.  Y and X are inverted in the save data so we subtract dx from y and store the result in the first coordinate.
                //dx and dy offset the result so the data is relative to cell A1 in Excel.
                basement[cask.Y - dx, cask.X - dy] = cask.ToMiniString();

                //Console "Debugging"
                Console.WriteLine(cask);
            }

            Console.WriteLine("Total Casks: {0}", casks.Count);
            OutputCSV(basement, "casks");

            Stardew.PauseConsole();
        }

        public static void PauseConsole()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static String DisplayQuality(string quality)
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

        private static void OutputCSV(Object[,] data, string csvname = "test")
        {
            using (StreamWriter outfile = new StreamWriter(String.Format("C:/Temp/{0}.csv", csvname)))
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
