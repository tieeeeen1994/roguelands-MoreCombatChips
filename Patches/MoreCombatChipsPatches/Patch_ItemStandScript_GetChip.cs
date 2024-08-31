using System;
using GadgetCore.API;
using HarmonyLib;
using TienContentMod.CombatChips;
using TienContentMod.Gadgets;
using TienContentMod.Services;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(ItemStandScript))]
    [HarmonyPatch("GetChip")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_ItemStandScript_GetChip
    {
        [HarmonyPrefix]
        public static bool Prefix(ItemStandScript __instance, ref int __result)
        {
            if (__instance.isAdvanced)
            {
                int randomNumber = UnityEngine.Random.Range(0, 100);
                if (randomNumber < 10)
                {
                    bool extraChecks(CombatChip cc) => cc.Stats == new EquipStats(0);
                    __result = ChipService.RandomlyGetIDFromAdvanced(extraChecks);
                    MoreCombatChips.Log($"Generating Item Stand with Chip ID {__result}");
                    return false;
                }
                else if (randomNumber < 20)
                {
                    bool extraChecks(CombatChip cc) => cc.Stats != new EquipStats(0);
                    __result = ChipService.RandomlyGetIDFromAdvanced(extraChecks);
                    MoreCombatChips.Log($"Generating Item Stand with Stat Boost Chip ID {__result}");
                    return false;
                }
            }
            return true;
        }
    }
}
