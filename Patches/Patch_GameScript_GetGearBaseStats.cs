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
                case ItemID.GadgetRPG:
                    __result = StatService.NewStats(VIT: 2, TEC: 6);
                    return false;
                case ItemID.RCK22:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(STR: 1);
                        return false;
                    }
                    goto default;
                case ItemID.FLWR08:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(DEX: 1);
                        return false;
                    }
                    goto default;
                case ItemID.BAT17:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(VIT: 1);
                        return false;
                    }
                    goto default;
                case ItemID.OBSIDIAN64:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(MAG: 1);
                        return false;
                    }
                    goto default;
                case ItemID.HELPR55:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(TEC: 1);
                        return false;
                    }
                    goto default;
                case ItemID.GUARDIAN07:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(FTH: 1);
                        return false;
                    }
                    goto default;
                case ItemID.SOLAR05:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(STR: 3);
                        return false;
                    }
                    goto default;
                case ItemID.PRISM88:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(STR: 2, VIT: 1, TEC: 1);
                        return false;
                    }
                    goto default;
                case ItemID.MONOLTH25:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(STR: 2, VIT: 2);
                        return false;
                    }
                    goto default;
                case ItemID.FARMHAND75:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(DEX: 3);
                        return false;
                    }
                    goto default;
                case ItemID.BULB88:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(DEX: 2, STR: 1, VIT: 1);
                        return false;
                    }
                    goto default;
                case ItemID.BTTRFLY8:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(DEX: 2, STR: 2);
                        return false;
                    }
                    goto default;
                case ItemID.DRAGON67:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(VIT: 3);
                        return false;
                    }
                    goto default;
                case ItemID.WYVRN77:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(VIT: 2, STR: 1, MAG: 1);
                        return false;
                    }
                    goto default;
                case ItemID.MEGAZORD36:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(VIT: 2, MAG: 2);
                        return false;
                    }
                    goto default;
                case ItemID.STEEL65:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(MAG: 3);
                        return false;
                    }
                    goto default;
                case ItemID.DIAMND66:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(MAG: 2, FTH: 1, TEC: 1);
                        return false;
                    }
                    goto default;
                case ItemID.BOGBOT67:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(MAG: 2, FTH: 2);
                        return false;
                    }
                    goto default;
                case ItemID.AIDBOT56:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(TEC: 3);
                        return false;
                    }
                    goto default;
                case ItemID.HELLBOT57:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(TEC: 2, VIT: 1, FTH: 1);
                        return false;
                    }
                    goto default;
                case ItemID.IBOT58:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(TEC: 2, VIT: 2);
                        return false;
                    }
                    goto default;
                case ItemID.WHITEMAG09:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(FTH: 3);
                        return false;
                    }
                    goto default;
                case ItemID.OVERSEER06:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(FTH: 2, MAG: 1, VIT: 1);
                        return false;
                    }
                    goto default;
                case ItemID.MRGRGRR05:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats(FTH: 2, VIT: 2);
                        return false;
                    }
                    goto default;
                case ItemID.GOLD15:
                    if (MoreCombatChips.DroidsRework)
                    {
                        __result = StatService.NewStats();
                        return false;
                    }
                    goto default;
                default:
                    return true;
            }
        }
    }
}
