using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.Services;
using UnityEngine;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(ItemStandScript))]
    [HarmonyPatch("GetChipCost")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
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