using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.Services;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This adds the combat chips from this mod to the item stands.
    /// </summary>
    [HarmonyPatch(typeof(ItemStandScript))]
    [HarmonyPatch("GetChip")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_ItemStandScript_GetChip
    {
        [HarmonyPrefix]
        public static bool Prefix(ItemStandScript __instance, ref int __result)
        {
            if (__instance.isAdvanced)
            {
                if (Util.RandomCheck(5))
                {
                    __result = ChipService.RandomlyGetIDFromAdvanced();
                    MoreCombatChips.Log($"Generating Item Stand with Chip ID {__result}");
                    return false;
                }
            }

            return true;
        }
    }
}
