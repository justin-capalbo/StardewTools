using System;
using Microsoft.Xna.Framework;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;

namespace DogLove
{
    /// <summary>The mod entry point.</summary>
    public class DogLoveMod : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            SaveEvents.AfterLoad += this.SaveEvents_AfterLoad;
            ControlEvents.KeyPressed += this.ControlEvents_KeyPress;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>The method invoked when the player presses a keyboard button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void ControlEvents_KeyPress(object sender, EventArgsKeyPressed e)
        {
            //this.Monitor.Log($"Player pressed {e.KeyPressed}.");

            if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.Tab)
            {
                Pet pet = Utility.getAllCharacters().OfType<Pet>().FirstOrDefault();
                if (pet == null)
                {
                    this.Monitor.Log("You don't have a pet.", LogLevel.Warn);
                    return;
                }

                bool wasPet = this.Helper.Reflection.GetPrivateValue<bool>(pet, "wasPetToday");

                string msg = "";
                if (wasPet)
                    msg = $"{pet.name} was pet today.";
                else
                    msg = $"{pet.name} was not pet today.";

                this.Monitor.Log(msg, LogLevel.Info);
                Game1.addHUDMessage(new HUDMessage(msg, 3) { noIcon = true, timeLeft = 3500f });
            }
        }

        /// <summary>The method called after the player loads their save.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            this.Monitor.Log($"Save file loaded.  This is a good time to do stuff.");
        }

    }
}