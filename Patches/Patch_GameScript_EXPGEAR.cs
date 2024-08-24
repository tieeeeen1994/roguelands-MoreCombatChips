using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;

namespace MoreCombatChips.Patches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("EXPGEAR")]
    [HarmonyGadget("More Combat Chips")]
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
