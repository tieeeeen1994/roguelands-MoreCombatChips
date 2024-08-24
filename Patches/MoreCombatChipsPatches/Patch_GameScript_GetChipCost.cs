using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetChipCost")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_GameScript_GetChipCost
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref int __result)
        {
            switch (id)
            {
                case CombatChipID.Quadracopter:
                    if (MoreCombatChips.QuadracopterCost)
                    {
                        __result = 30;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                default:
                    return true;
            }
        }
    }
}
