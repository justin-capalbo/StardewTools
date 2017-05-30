using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Objects;

namespace Artisan
{
    public class ArtisanMod : Mod
    {
        static HashSet<GameLocation> SweepedLocations = new HashSet<GameLocation>();

        /// <summary>Run when the mod is loaded.</summary>
        /// <param name="helper">The mod helper./param>
        public override void Entry(IModHelper helper)
        {
            SaveEvents.AfterLoad += OnAfterLoad;
        }

        /// <summary>Trigger the action for the tile under the cursor, if applicable.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        void OnAfterLoad(object sender, EventArgs e)
        {
            ControlEvents.MouseChanged += ControlEvents_MouseChanged;
        }

        /// <summary>Trigger the action for the tile under the cursor, if applicable.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ControlEvents_MouseChanged(object sender, EventArgsMouseStateChanged e)
        {
            if (Context.IsWorldReady && Game1.activeClickableMenu == null)
                this.TryActionUnderCursor(e);
        }

        /// <summary>Trigger the action for the tile under the cursor, if applicable.</summary>
        private bool TryActionUnderCursor(EventArgsMouseStateChanged e)
        {
            // skip if player didn't right-click
            if (e.PriorState.RightButton == ButtonState.Pressed || e.NewState.RightButton != ButtonState.Pressed)
                return false;

            // get tile under cursor
            Vector2 tile = Game1.currentCursorTile;

            //tile = Utility.tileWithinRadiusOfPlayer((int)tile.X, (int)tile.Y, 1, Game1.player)
            //    ? tile
            //    : Game1.player.GetGrabTile();

            // get cask on tile
            StardewValley.Object obj;
            if (!Game1.currentLocation.objects.TryGetValue(tile, out obj) || !(obj is Cask cask))
                return false;

            // do stuff with cask
            Cask theCask = (Cask)obj;
            DisplayCask(theCask);
            DebugCask(theCask);

            return true;
        }

        private void DisplayCask(Cask cask)
        {
            string days = Math.Round(cask.daysToMature / cask.agingRate, 2).ToString();
            string stars = new String('=', cask.heldObject.quality);
            string msg = String.Format("{0} =x{2}\nDays Left: {1}",
                         cask.heldObject.DisplayName, days, cask.heldObject.quality);

            Game1.addHUDMessage(new HUDMessage(msg, 3) { noIcon = true, timeLeft = 3500f });
        }

        private void DebugCask(Cask cask)
        {
            this.Monitor.Log(String.Format("==Cask==\n" +
                            "Item: {3} \n" +
                            "Located at: ({0},{1})\n" +
                            "Days Left: {2}\n", cask.TileLocation.X, cask.TileLocation.Y, cask.daysToMature / cask.agingRate, cask.heldObject.DisplayName),
                            LogLevel.Info);
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
                }
            }
            SweepedLocations.Add(Game1.currentLocation);
        }
    }
 }
