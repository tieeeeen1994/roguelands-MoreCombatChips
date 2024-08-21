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
            switch (a)
            {
                case AugmentID.RebellionHeadpiece:
                    __result = "Headgear worn by a true Rebel of the Starlight Empire.\n" +
                               "Max HP is now 75. Gain 2 DEX per level.";
                    break;
                case AugmentID.CreatorMask:
                    __result = "A mask crafted by the fabric of time and space.\n" +
                               "Upon level up, gain 1 MAG with a 25% chance or 1 FTH with a 75% chance.";
                    break;
                case AugmentID.EyepodHat:
                    __result = "A hat made from the remains of an Eyepod. Yikes.\n" +
                               "On level up, stats gained are double, but equipment are only 50% effective.";
                    break;
                case AugmentID.GasMask:
                    __result = "Gray Enigma's standard gas mask to protect the wearer from noxious gases.\n" +
                               "When using a Combat Chip with insufficent mana, consume Stamina instead.";
                    break;
                default:
                    return true;
            }
            return false;
        }
    }
}
