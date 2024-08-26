using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("EXPGEAR")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_GameScript_EXPGEAR
    {
        [HarmonyPrefix]
        public static bool Prefix(ref int[] a)
        {
            switch (Menuu.curAugment)
            {
                case AugmentID.SlimeHat:
                    a[1] *= 2;
                    break;
            }
            return true;
        }
    }
}