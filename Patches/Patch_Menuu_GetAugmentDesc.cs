using GadgetCore.API;
using HarmonyLib;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will change the description of augments.
    /// </summary>
    [HarmonyPatch(typeof(Menuu))]
    [HarmonyPatch("GetAugmentDesc")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_Menuu_GetAugmentDesc
    {
        [HarmonyPrefix]
        public static bool Prefix(int a, ref string __result)
        {
            if (a == 22) // Rebellion Headpiece
            {
                __result = "Headgear worn by a true Rebel of the Starlight Empire.\nMax HP is now 75. Gain 2 DEX per level.";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}