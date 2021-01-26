using StardewModdingAPI;
using Harmony;
using System;
using StardewValley.BellsAndWhistles;


namespace CasualNPCNicknames
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod //,IAssetLoader, IAssetEditor
    {
        public static IMonitor PMonitor;
        public static IModHelper PHelper;
        public static ModConfig config;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            config = Helper.ReadConfig<ModConfig>();

            if (!config.EnableMod)
                return;

            PMonitor = Monitor;
            PHelper = helper;
            Random randNr = new Random();

            Patches.Initialize(Monitor);

            var harmony = HarmonyInstance.Create("IsTotallyUniqueID");

            harmony.Patch(
              original: AccessTools.Method(typeof(SpriteText), nameof(SpriteText.drawStringHorizontallyCenteredAt)),
              prefix: new HarmonyMethod(typeof(Patches), nameof(Patches.DrawString_prefix))
           );

            /*
            harmony.Patch(
               original: AccessTools.Method(typeof(NPC), nameof(NPC.marriageDuties)),
               postfix: new HarmonyMethod(typeof(NPCPatches), nameof(NPCPatches.NPC_marriageDuties_Postfix))
            );
            */

        }
    }
}