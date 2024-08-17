using GadgetCore.API;
using HarmonyLib;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will modify costs of vanilla Combat Chips.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetChipDesc")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_GetChipDesc
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref string __result)
        {
            if (id == 22) // Quadracopter
            {
                __result = "Summon a Quadracopter that shoots 15 projectiles.\nScales with 3x TEC.";
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}