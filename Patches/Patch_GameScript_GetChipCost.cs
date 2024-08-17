using GadgetCore.API;
using HarmonyLib;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will modify costs of vanilla Combat Chips.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetChipCost")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_GetChipCost
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int __result)
        {
            if (id == 22) // Quadracopter
            {
                __result = 30;
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}