using GadgetCore.API;
using HarmonyLib;

namespace MoreCombatChips.Patches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetGearBaseStats")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_GetGearBaseStats
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int[] __result)
        {
            if (id == 473) // Gadget RPG
            {
                __result = new int[] { 2, 0, 0, 6, 0, 0 };
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}