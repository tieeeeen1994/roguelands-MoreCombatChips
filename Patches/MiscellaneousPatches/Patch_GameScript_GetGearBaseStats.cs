using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;
using TienContentMod.Services;

namespace TienContentMod.Patches.MiscellaneousPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetGearBaseStats")]
    [HarmonyGadget(Miscellaneous.GADGET_NAME)]
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
                default:
                    return true;
            }
        }
    }
}
