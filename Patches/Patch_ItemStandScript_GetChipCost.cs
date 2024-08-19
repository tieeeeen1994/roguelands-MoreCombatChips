using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.Services;
using UnityEngine;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will initialize the cost of the combat chips in the item stands.
    /// </summary>
    [HarmonyPatch(typeof(ItemStandScript))]
    [HarmonyPatch("GetChipCost")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_ItemStandScript_GetChipCost
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int __result)
        {
            int index = ChipService.GetIndexFromList(id);
            if (index != -1)
            {
                __result = Random.Range(11, 16);
                return false;
            }
            return true;
        }
    }
}
