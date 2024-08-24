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
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_RefreshChipStats
    {
        private const bool DISABLED = false;

        [HarmonyPostfix]
        public static void Postfix(GameScript __instance)
        {
            if (__instance.GetFieldValue("CHIPSTAT") is int[] chipStats)
            {
                switch (Menuu.curAugment)
                {
                    case AugmentID.EyepodHat:
                        if (DISABLED && MoreCombatChips.EyepodHatChange)
                        {
                            for (int i = 0; i < chipStats.Length; i++)
                            {
                                chipStats[i] /= 2;
                            }
                        }
                        break;
                    case AugmentID.ShroomHat:
                        if (MoreCombatChips.ShroomHatChange)
                        {
                            for (int i = 0; i < chipStats.Length; i++)
                            {
                                chipStats[i] *= 2;
                            }
                        }
                        break;
                }
                __instance.SetFieldValue("CHIPSTAT", chipStats);
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
