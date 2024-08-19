using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;
using MoreCombatChips.Services;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will change the base stats of weapons or gears.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetGearBaseStats")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_GetGearBaseStats
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int[] __result)
        {
            switch (id)
            {
                case ItemID.GadgetRPG: // Gadget RPG
                    __result = StatService.NewStats(VIT: 2, TEC: 6);
                    return false;
                default:
                    return true;
            }
        }
    }
}
