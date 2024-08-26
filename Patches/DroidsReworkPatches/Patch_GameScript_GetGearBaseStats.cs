using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;
using TienContentMod.Services;

namespace TienContentMod.Patches.DroidsReworkPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetGearBaseStats")]
    [HarmonyGadget(DroidsRework.GADGET_NAME)]
    public static class Patch_GameScript_GetGearBaseStats
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int[] __result)
        {
            switch (id)
            {
                case ItemID.RCK22:
                    __result = StatService.NewStats(STR: 1);
                    return false;

                case ItemID.FLWR08:
                    __result = StatService.NewStats(DEX: 1);
                    return false;

                case ItemID.BAT17:
                    __result = StatService.NewStats(VIT: 1);
                    return false;

                case ItemID.OBSIDIAN64:
                    __result = StatService.NewStats(MAG: 1);
                    return false;

                case ItemID.HELPR55:
                    __result = StatService.NewStats(TEC: 1);
                    return false;

                case ItemID.GUARDIAN07:
                    __result = StatService.NewStats(FTH: 1);
                    return false;

                case ItemID.SOLAR05:
                    __result = StatService.NewStats(STR: 3);
                    return false;

                case ItemID.PRISM88:
                    __result = StatService.NewStats(STR: 2, VIT: 1, TEC: 1);
                    return false;

                case ItemID.MONOLTH25:
                    __result = StatService.NewStats(STR: 2, VIT: 2);
                    return false;

                case ItemID.FARMHAND75:
                    __result = StatService.NewStats(DEX: 3);
                    return false;

                case ItemID.BULB88:
                    __result = StatService.NewStats(DEX: 2, STR: 1, VIT: 1);
                    return false;

                case ItemID.BTTRFLY8:
                    __result = StatService.NewStats(DEX: 2, STR: 2);
                    return false;

                case ItemID.DRAGON67:
                    __result = StatService.NewStats(VIT: 3);
                    return false;

                case ItemID.WYVRN77:
                    __result = StatService.NewStats(VIT: 2, STR: 1, MAG: 1);
                    return false;

                case ItemID.MEGAZORD36:
                    __result = StatService.NewStats(VIT: 2, MAG: 2);
                    return false;

                case ItemID.STEEL65:
                    __result = StatService.NewStats(MAG: 3);
                    return false;

                case ItemID.DIAMND66:
                    __result = StatService.NewStats(MAG: 2, FTH: 1, TEC: 1);
                    return false;

                case ItemID.BOGBOT67:
                    __result = StatService.NewStats(MAG: 2, FTH: 2);
                    return false;

                case ItemID.AIDBOT56:
                    __result = StatService.NewStats(TEC: 3);
                    return false;

                case ItemID.HELLBOT57:
                    __result = StatService.NewStats(TEC: 2, VIT: 1, FTH: 1);
                    return false;

                case ItemID.IBOT58:
                    __result = StatService.NewStats(TEC: 2, VIT: 2);
                    return false;

                case ItemID.WHITEMAG09:
                    __result = StatService.NewStats(FTH: 3);
                    return false;

                case ItemID.OVERSEER06:
                    __result = StatService.NewStats(FTH: 2, MAG: 1, VIT: 1);
                    return false;

                case ItemID.MRGRGRR05:
                    __result = StatService.NewStats(FTH: 2, VIT: 2);
                    return false;

                case ItemID.GOLD15:
                    __result = StatService.NewStats(TEC: 1);
                    return false;

                default:
                    return true;
            }
        }
    }
}
