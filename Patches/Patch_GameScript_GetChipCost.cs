using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;

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
            switch (id)
            {
                case CombatChipID.Quadracopter:
                    if (MoreCombatChips.QuadracopterCost)
                    {
                        __result = 30;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                default:
                    return true;
            }
        }
    }
}
