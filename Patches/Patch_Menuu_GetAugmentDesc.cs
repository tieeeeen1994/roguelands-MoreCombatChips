using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;

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
            switch ((AugmentID)a)
            {
                case AugmentID.RebellionHeadpiece:
                    __result = "Headgear worn by a true Rebel of the Starlight Empire.\nMax HP is now 75. Gain 2 DEX per level.";
                    break;
                case AugmentID.CreatorMask:
                    __result = "A mask crafted by the fabric of time and space.\n" +
                               "Upon level up, gain 1 MAG with a 25% chance or 1 FTH with a 75% chance.";
                    break;
                default:
                    return true;
            }
            return false;
        }
    }
}