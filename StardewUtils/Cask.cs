using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastium.StardewTools.StardewUtils
{
    class Cask
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public string ProductName { get; private set; }
        public string Quality { get; private set; }
        public string DisplayQuality { get { return Stardew.DisplayQuality(this.Quality); } }
        public double DaysToIridium { get; private set; }

        public Cask(XElement caskElement)
        {
            //Data about our cask
            var location = caskElement.Descendants("tileLocation").First();
            this.X = Int32.Parse(location.Descendants("X").First().Value);
            this.Y = Int32.Parse(location.Descendants("Y").First().Value);

            var agingRate = Double.Parse(caskElement.Descendants("agingRate").First().Value);
            this.DaysToIridium = Math.Round(Double.Parse(caskElement.Descendants("daysToMature").First().Value) / agingRate, 2);

            //Data about what's in the cask
            var heldObject = caskElement.Descendants("heldObject");
            this.ProductName = heldObject.Descendants("DisplayName").First().Value;
            this.Quality = heldObject.Descendants("quality").First().Value;
        }

        public string ToMiniString()
        {
            return String.Format("\"{0} {1}\n{2}\"", this.DisplayQuality, this.ProductName, this.DaysToIridium);
        }

        public override string ToString()
        {
            return String.Format("==Cask==\n" +
                                 "Item: {4} {2}\n" +
                                 "Located at: ({0},{1})\n" +
                                 "Days Left: {3}\n", this.X, this.Y, this.ProductName, this.DaysToIridium, this.DisplayQuality);
        }

        public static List<Cask> BuildCaskList(IEnumerable<XElement> caskObjects)
        {
            List<Cask> caskList = new List<Cask>();
            foreach (var caskElement in caskObjects)
            {
                caskList.Add(new Cask(caskElement));
            }
            return caskList;
        }
    }
}
