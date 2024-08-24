using GadgetCore.API;
using HarmonyLib;
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
