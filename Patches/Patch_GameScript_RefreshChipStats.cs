using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using System;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will add effects for chips providing stats.
    /// </summary>
    [HarmonyAfter("GadgetCore.core")]
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("RefreshChipStats")]
    [HarmonyGadget("DISABLED: More Combat Chips")]
    public static class Patch_GameScript_RefreshChipStats
    {
        [HarmonyPostfix]
        public static void Postfix(GameScript __instance)
        {
            if (__instance.GetFieldValue("CHIPSTAT") is int[] chipStats)
            {
                switch (Menuu.curAugment)
                {
                    case AugmentID.EyepodHat:
                        if (MoreCombatChips.EyepodHatChange)
                        {
                            for (int i = 0; i < chipStats.Length; i++)
                            {
                                chipStats[i] /= 2;
                            }
                            __instance.SetFieldValue("CHIPSTAT", chipStats);
                        }
                        break;
                }
            }
            else
            {
                string message = "Patch_GameScript_RefreshChipStats: GameScript.CHIPSTAT not found.";
                MoreCombatChips.Error(message);
                throw new Exception(message);
            }
        }
    }
}
