using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyAfter("GadgetCore.core")]
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("RefreshChipStats")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_GameScript_RefreshChipStats
    {
        private static readonly bool DISABLED = false;

        [HarmonyPostfix]
        public static void Postfix(GameScript __instance)
        {
            if (__instance.GetFieldValue("CHIPSTAT") is int[] chipStats)
            {
                switch (Menuu.curAugment)
                {
                    case AugmentID.EyepodHat:
                        if (DISABLED)
                        {
                            for (int i = 0; i < chipStats.Length; i++)
                            {
                                chipStats[i] /= 2;
                            }
                        }
                        break;

                    case AugmentID.ShroomHat:
                        for (int i = 0; i < chipStats.Length; i++)
                        {
                            chipStats[i] *= 2;
                        }
                        break;
                }
                __instance.SetFieldValue("CHIPSTAT", chipStats);
            }
            else
            {
                string message = "Patch_GameScript_RefreshChipStats: GameScript.CHIPSTAT not found.";
                BetterAugments.Error(message);
                throw new Exception(message);
            }
        }
    }
}