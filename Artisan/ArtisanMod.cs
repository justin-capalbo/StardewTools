using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Objects;
using StardewValley.Characters;

namespace Artisan
{
    public class ArtisanMod : Mod
    {
        static HashSet<GameLocation> SweepedLocations = new HashSet<GameLocation>();

        public override void Entry(IModHelper helper)
        {
            SaveEvents.AfterLoad += OnAfterLoad;
        }

        void OnAfterLoad(object sender, EventArgs e)
        {
        }
        

        private void CaskSweep()
        {
            if (SweepedLocations.Contains(Game1.currentLocation))
                return;

            foreach (var o in Game1.currentLocation.objects.ToArray())
            {
                if (o.Value is Cask)
                {
                    Cask c = (Cask)o.Value;
                    this.Monitor.Log(String.Format("==Cask==\n" +
                                        "Item: {3} \n" +
                                        "Located at: ({0},{1})\n" +
                                        "Days Left: {2}\n", c.TileLocation.X, c.TileLocation.Y, c.daysToMature / c.agingRate, c.heldObject.DisplayName), 
                                     LogLevel.Info);
                }
            }
            SweepedLocations.Add(Game1.currentLocation);
        }
    }
 }
