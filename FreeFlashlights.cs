using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeFlashlights
{
    [BepInPlugin(modGUID, modName, modSemVer)]
    public class FreeFlashlights : BaseUnityPlugin
    {
        private const string modGUID = "evelritual.FreeFlashlights";
        private const string modName = "Free Flashlights";
        private const string modSemVer = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static FreeFlashlights Instance;
        internal ManualLogSource logger;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            harmony.PatchAll();

            logger.LogInfo("FreeFlashlights Loaded!");
        }
    }
}

namespace FreeFlashlights.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class pricePatch
    {
        [HarmonyPatch("SetItemSales")]
        [HarmonyPostfix]
        private static void StorePrices(ref Item[] ___buyableItemsList)
        {
            // Flashlight
            ___buyableItemsList[1].creditsWorth = 0;
            // Pro Flashlight
            ___buyableItemsList[4].creditsWorth = 0;
        }
    }
}
